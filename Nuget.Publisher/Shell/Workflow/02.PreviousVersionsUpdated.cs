namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	using Newtonsoft.Json;

	public class PreviousVersionsUpdated : FluentWorkflow
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public PreviousVersionsUpdated(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public UpdatePackagesDumped DumpUpdatedPackages()
		{
			var json = JsonConvert.SerializeObject(packageUpdateResults, Formatting.Indented);

			Console.WriteLine(json);

			return new UpdatePackagesDumped();
		}
	}
}