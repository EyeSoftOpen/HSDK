namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;
	using Build;

	public class UpdatePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly BuildAndRevision buildAndRevision;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly SolutionSystemInfo solutionSystemInfo;

		public UpdatePreviousVersionsStep(SolutionSystemInfo solutionSystemInfo, NugetPackageResultCollection nugetPackageResultCollection, BuildAndRevision buildAndRevision)
		{
			this.solutionSystemInfo = solutionSystemInfo;

			this.buildAndRevision = buildAndRevision;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public DumpUpdatedPackagesStep UpdatePreviousVersions()
		{
			return new DumpUpdatedPackagesStep(solutionSystemInfo, nugetPackageResultCollection, buildAndRevision);
		}
	}
}