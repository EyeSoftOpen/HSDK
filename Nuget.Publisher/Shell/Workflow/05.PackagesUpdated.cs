namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class PackagesUpdated : FluentWorkflow
	{
		private readonly string solutionPath;

		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public PackagesUpdated(string solutionPath, IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.solutionPath = solutionPath;

			this.packageUpdateResults = packageUpdateResults;
		}

		public SolutionBuilt BuildSolution()
		{
			MsBuild.Build(solutionPath);

			return new SolutionBuilt(packageUpdateResults);
		}
	}
}