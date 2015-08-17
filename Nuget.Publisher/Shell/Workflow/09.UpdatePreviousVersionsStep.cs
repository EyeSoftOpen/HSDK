namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdatePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public UpdatePreviousVersionsStep(NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public DumpUpdatedPackagesStep UpdatePreviousVersions()
		{
			return new DumpUpdatedPackagesStep(nugetPackageResultCollection);
		}
	}
}