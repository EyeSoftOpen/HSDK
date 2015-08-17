namespace EyeSoft.Nuget.Publisher.Shell
{
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	using Newtonsoft.Json;

	public class DumpUpdatedPackagesStep : FluentWorkflowStep
	{
		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		public DumpUpdatedPackagesStep(NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public WaitStep DumpUpdatedPackages()
		{
			var json = JsonConvert.SerializeObject(nugetPackageResultCollection, Formatting.Indented);

			ConsoleHelper.WriteLine(json);

			return new WaitStep();
		}
	}
}