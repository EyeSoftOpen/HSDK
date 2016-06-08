namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;
	using EyeSoft.Nuget.Publisher.Core.Nuget;

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
			return new UpdatePreviousVersionsStep(buildAndRevision, solutionSystemInfo, nugetPackageResultCollection);
		}
	}
}