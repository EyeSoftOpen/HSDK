namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class NugetPackagesPublished : FluentWorkflow
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public NugetPackagesPublished(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public PreviousVersionsUpdated UpdatePreviousVersions()
		{
			return new PreviousVersionsUpdated(packageUpdateResults);
		}
	}
}