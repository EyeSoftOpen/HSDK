namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Diagnostics;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class CompileNugetPackagesStep : FluentWorkflowStep
	{
		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public CompileNugetPackagesStep(NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public PublishNugetPackagesStep CompileNugetPackages()
		{
			var nuspecFiles = nugetPackageResultCollection.NugetPackages.Select(x => x.NuspecFile.FullName).ToArray();

			foreach (var nuspec in nuspecFiles)
			{
				Process.Start("");
			}

			return new PublishNugetPackagesStep(nugetPackageResultCollection);
		}
	}
}