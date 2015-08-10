namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdatePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public UpdatePreviousVersionsStep(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public DumpUpdatedPackagesStep UpdatePreviousVersions()
		{
			return new DumpUpdatedPackagesStep(packageUpdateResults);
		}
	}
}