namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using Newtonsoft.Json;

	public class BuildAndRevisionGenerated : FluentWorkflow
	{
		private readonly string solutionFolderPath;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		public BuildAndRevisionGenerated(string solutionFolderPath, IEnumerable<string> packagesId, BuildAndRevision buildAndRevision)
		{
			this.solutionFolderPath = solutionFolderPath;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
		}

		public PreviousVersionsRetrieved GetPreviousVersions()
		{
			var jsonPath = Path.Combine(solutionFolderPath, "Libraries", "Hsdk.Packages.Version.json");

			var json = Storage.ReadAllText(jsonPath);

			var previousVersions = JsonConvert.DeserializeObject<IReadOnlyDictionary<string, string>>(json);

			return new PreviousVersionsRetrieved(solutionFolderPath, packagesId, buildAndRevision, previousVersions);
		}
	}
}