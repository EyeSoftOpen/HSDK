using System.Collections.Generic;
using EyeSoft.Nuget.Publisher.Shell.Nuget;

namespace EyeSoft.Nuget.Publisher.Shell
{
	public class CompileNugetPackagesStep : FluentWorkflowStep
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public CompileNugetPackagesStep(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public PublishNugetPackagesStep CompileNugetPackages()
		{
			return new PublishNugetPackagesStep(packageUpdateResults);
		}
	}
}