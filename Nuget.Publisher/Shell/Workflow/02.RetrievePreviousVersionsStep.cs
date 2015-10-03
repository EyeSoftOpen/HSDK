namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	using Newtonsoft.Json;
	using System.Linq;
	using System;

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

			var jsonsPath = Path.Combine(solutionFolderPath, "Libraries");

			var previousVersions = new Dictionary<string, string>();

			var savedDictionaries = new DirectoryInfo(jsonsPath)
				.GetFiles("*.json")
				.Select(x => JsonConvert.DeserializeObject<IReadOnlyDictionary<string, string>>(Storage.ReadAllText(x.FullName)));

			foreach (var dictionary in savedDictionaries)
			{
				foreach (var package in dictionary)
				{
					if(previousVersions.ContainsKey(package.Key))
					{
						var dictionaryVersion = new Version(previousVersions[package.Key]);

						var savedVersion = new Version(package.Value);

						if(savedVersion> dictionaryVersion)
						{
							previousVersions[package.Key] = savedVersion.ToString();
						}
					}
					else
					{
						previousVersions.Add(package.Key, package.Value);
					}
				}
			}

			return new CollectPackagesFromSolutionStep(
				solutionSystemInfo,
				packagesId,
				buildAndRevision,
				previousVersions);
		}
	}
}