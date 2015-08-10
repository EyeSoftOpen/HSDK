namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class BuildSolutionStep : FluentWorkflowStep
	{
		private readonly string solutionPath;

		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public BuildSolutionStep(string solutionPath, IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.solutionPath = solutionPath;

			this.packageUpdateResults = packageUpdateResults;
		}

		public CompileNugetPackagesStep BuildSolution()
		{
			MsBuild.Build(solutionPath);

			return new CompileNugetPackagesStep(packageUpdateResults);
		}
	}
}