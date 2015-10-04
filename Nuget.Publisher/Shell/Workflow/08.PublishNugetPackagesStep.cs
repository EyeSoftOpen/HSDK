namespace EyeSoft.Nuget.Publisher.Shell
{
	using Build;

	using EyeSoft.Nuget.Publisher.Shell.Core;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class PublishNugetPackagesStep : FluentWorkflowStep
	{
		private readonly BuildAndRevision buildAndRevision;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly SolutionSystemInfo solutionSystemInfo;

		public PublishNugetPackagesStep(SolutionSystemInfo solutionSystemInfo, NugetPackageResultCollection nugetPackageResultCollection, BuildAndRevision buildAndRevision)
		{
			this.solutionSystemInfo = solutionSystemInfo;

			this.buildAndRevision = buildAndRevision;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public UpdatePreviousVersionsStep PublishNugetPackages()
		{
			return new UpdatePreviousVersionsStep(this.buildAndRevision, this.solutionSystemInfo, this.nugetPackageResultCollection);
		}
	}
}