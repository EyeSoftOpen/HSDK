namespace EyeSoft.Nuget.Publisher.Shell
{
	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class BuildSolutionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly BuildAndRevision buildAndRevision;

		public BuildSolutionStep(SolutionSystemInfo solutionSystemInfo, NugetPackageResultCollection nugetPackageResultCollection, BuildAndRevision buildAndRevision)
		{
            this.buildAndRevision = buildAndRevision;

            this.solutionSystemInfo = solutionSystemInfo;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public CompileNugetPackagesStep BuildSolution()
		{
			MsBuild.Build(solutionSystemInfo.FilePath);

			return new CompileNugetPackagesStep(solutionSystemInfo, nugetPackageResultCollection, buildAndRevision);
		}
	}
}