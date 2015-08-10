namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	using Newtonsoft.Json;

	public class DumpUpdatedPackagesStep : FluentWorkflowStep
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public DumpUpdatedPackagesStep(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public WaitStep DumpUpdatedPackages()
		{
			var json = JsonConvert.SerializeObject(packageUpdateResults, Formatting.Indented);

			ConsoleHelper.WriteLine(json);

			return new WaitStep();
		}
	}
}