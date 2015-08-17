namespace EyeSoft.Nuget.Publisher.Shell
{
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class PublishNugetPackagesStep : FluentWorkflowStep
	{
		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public PublishNugetPackagesStep(NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public UpdatePreviousVersionsStep PublishNugetPackages()
		{
			return new UpdatePreviousVersionsStep(nugetPackageResultCollection);
		}
	}
}