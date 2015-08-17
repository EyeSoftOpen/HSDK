namespace EyeSoft.Nuget.Publisher.Shell
{
	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class BuildSolutionStep : FluentWorkflowStep
	{
		private readonly string solutionPath;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public BuildSolutionStep(string solutionPath, NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.solutionPath = solutionPath;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public CompileNugetPackagesStep BuildSolution()
		{
			MsBuild.Build(solutionPath);

			return new CompileNugetPackagesStep(nugetPackageResultCollection);
		}
	}
}