namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;
	using EyeSoft.Nuget.Publisher.Core.Nuget;

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