namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class PublishNugetPackagesStep : FluentWorkflowStep
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public PublishNugetPackagesStep(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public UpdatePreviousVersionsStep PublishNugetPackages()
		{
			return new UpdatePreviousVersionsStep(packageUpdateResults);
		}
	}
}