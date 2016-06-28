namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using System.IO;

	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;
	using EyeSoft.Nuget.Publisher.Core.Nuget;

	public class BuildSolutionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly BuildAndRevision buildAndRevision;

		public BuildSolutionStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.buildAndRevision = buildAndRevision;

			this.solutionSystemInfo = solutionSystemInfo;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public CompileNugetPackagesStep BuildSolution(bool logEnabled)
		{
			new MsBuild(solutionSystemInfo.FilePath, new FileInfo(solutionSystemInfo.FilePath).Directory.FullName, logEnabled).Build();

			return new CompileNugetPackagesStep(buildAndRevision, solutionSystemInfo, nugetPackageResultCollection);
		}
	}
}