namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;
	using EyeSoft.Nuget.Publisher.Core.Nuget;

	using Newtonsoft.Json;

	public class DumpUpdatedPackagesStep : FluentWorkflowStep
	{
		private readonly BuildAndRevision buildAndRevision;

		private readonly NugetPackageResultCollection nugetPackageResultCollection;

		private readonly SolutionSystemInfo solutionSystemInfo;

		public DumpUpdatedPackagesStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			NugetPackageResultCollection nugetPackageResultCollection)
		{
			this.solutionSystemInfo = solutionSystemInfo;

			this.buildAndRevision = buildAndRevision;

			this.nugetPackageResultCollection = nugetPackageResultCollection;
		}

		public WaitStep DumpUpdatedPackages()
		{
			var json = JsonConvert.SerializeObject(nugetPackageResultCollection, Formatting.Indented);

			ConsoleHelper.WriteLine(json);

			var solutionFolderPath = solutionSystemInfo.FolderPath;

			var jsonPath = Path.Combine(solutionFolderPath, "Libraries", $"Hsdk.Packages.Version.{buildAndRevision.Build}.{buildAndRevision.Revision}.json");

			Storage.CreateDirectory(new FileInfo(jsonPath).Directory.FullName);

			var newVersions = nugetPackageResultCollection.NewPackagesVersions.ToDictionary(k => k.Key, v => v.Value.ToString());

			var newJsonVersions = JsonConvert.SerializeObject(newVersions, Formatting.Indented);

			Storage.WriteAllText(jsonPath, newJsonVersions);

			return new WaitStep();
		}
	}
}