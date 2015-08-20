namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Diagnostics;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class CompileNugetPackagesStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public CompileNugetPackagesStep(SolutionSystemInfo solutionSystemInfo, NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.solutionSystemInfo = solutionSystemInfo;
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

			foreach (var nuspecArgument in nuspecArguments)
			{
				ProcessHelper.Start(nugetExePath, nuspecArgument, nugetCompilePath);
			}

			return new PublishNugetPackagesStep(nugetPackageResultCollection);
		}
	}
}