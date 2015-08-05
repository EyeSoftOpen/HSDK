namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class SolutionBuilt : FluentWorkflow
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public SolutionBuilt(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public NugetPackagesPublished PublishNugetPackages()
		{
			return new NugetPackagesPublished(packageUpdateResults);
		}
	}
}