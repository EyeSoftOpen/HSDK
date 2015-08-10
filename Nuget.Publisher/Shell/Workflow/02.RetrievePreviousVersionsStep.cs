namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using Newtonsoft.Json;

	public class RetrievePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly string solutionPath;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		public RetrievePreviousVersionsStep(
			string solutionPath, IEnumerable<string> packagesId, BuildAndRevision buildAndRevision)
		{
			this.solutionPath = solutionPath;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
		}

		public CollectPackagesFromSolutionStep RetrievePreviousVersions()
		{
			var solutionFolderPath = new FileInfo(solutionPath).Directory.FullName;

			var jsonPath = Path.Combine(solutionFolderPath, "Libraries", "Hsdk.Packages.Version.json");

			var json = Storage.ReadAllText(jsonPath);

			var previousVersions = JsonConvert.DeserializeObject<IReadOnlyDictionary<string, string>>(json);

			return new CollectPackagesFromSolutionStep(
				solutionPath,
				solutionFolderPath,
				packagesId,
				buildAndRevision,
				previousVersions);
		}
	}
}