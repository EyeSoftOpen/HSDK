namespace EyeSoft.Nuget.Publisher.Shell
{

	using Build;
	using EyeSoft.Nuget.Publisher.Shell.Core;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdatePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly BuildAndRevision buildAndRevision;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly SolutionSystemInfo solutionSystemInfo;

		public UpdatePreviousVersionsStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.solutionSystemInfo = solutionSystemInfo;

			this.buildAndRevision = buildAndRevision;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public DumpUpdatedPackagesStep UpdatePreviousVersions()
		{
			return new DumpUpdatedPackagesStep(buildAndRevision, solutionSystemInfo, nugetPackageResultCollection);
		}
	}
}