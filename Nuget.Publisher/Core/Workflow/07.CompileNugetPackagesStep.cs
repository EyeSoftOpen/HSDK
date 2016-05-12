namespace EyeSoft.Nuget.Publisher.Shell.Workflow
{
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Core;
	using EyeSoft.Nuget.Publisher.Shell.Diagnostics;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class CompileNugetPackagesStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly BuildAndRevision buildAndRevision;

		public CompileNugetPackagesStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.buildAndRevision = buildAndRevision;
			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public PublishNugetPackagesStep CompileNugetPackages()
		{
			var nugetExePath = Path.Combine(solutionSystemInfo.FolderPath, @".nuget\NuGet.exe");

			var nuspecFiles = nugetPackageResultCollection.NugetPackages.Select(x => x.ProjectFile).ToArray();

			var nugetCompilePath = Path.Combine(solutionSystemInfo.FolderPath, "Nuget.Packages");

			Directory.CreateDirectory(nugetCompilePath);

			var nuspecArguments =
				nuspecFiles
					.Select(nuspecFile => $"pack -Prop Configuration=Release -IncludeReferencedProjects \"{nuspecFile.FullName}\"")
					.ToArray();

			foreach (var nuspecArgument in nuspecArguments.AsParallel())
			{
				ProcessHelper.Start(nugetExePath, nuspecArgument, nugetCompilePath, false);
			}

			var updatedPackages = nugetPackageResultCollection.NugetPackages.Select(x => x.Title).ToArray();

			var nugetPushArguments = new DirectoryInfo(nugetCompilePath)
				.GetFiles()
				.Where(n => n.Name.EndsWith($"{buildAndRevision.Build}.{buildAndRevision.Revision}.nupkg") && updatedPackages.Any(x => n.Name.StartsWith(x)))
				.Select(nuspecFile => $"push {nuspecFile.FullName}");

			foreach (var nuspecArgument in nugetPushArguments.AsParallel())
			{
				ProcessHelper.Start(nugetExePath, nuspecArgument, nugetCompilePath, false);
			}

			return new PublishNugetPackagesStep(solutionSystemInfo, nugetPackageResultCollection, buildAndRevision);
		}
	}
}