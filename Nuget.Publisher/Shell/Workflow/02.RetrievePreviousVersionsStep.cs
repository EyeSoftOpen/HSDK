namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	using Newtonsoft.Json;

	public class RetrievePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		public RetrievePreviousVersionsStep(
			SolutionSystemInfo solutionSystemInfo, IEnumerable<string> packagesId, BuildAndRevision buildAndRevision)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
		}

		public CollectPackagesFromSolutionStep RetrievePreviousVersions()
		{
			var solutionFolderPath = solutionSystemInfo.FolderPath;

			var jsonPath = Path.Combine(solutionFolderPath, "Libraries", "Hsdk.Packages.Version.json");

			var json = Storage.ReadAllText(jsonPath);

			var previousVersions = JsonConvert.DeserializeObject<IReadOnlyDictionary<string, string>>(json);

			return new CollectPackagesFromSolutionStep(
				solutionSystemInfo,
				packagesId,
				buildAndRevision,
				previousVersions);
		}
	}
}