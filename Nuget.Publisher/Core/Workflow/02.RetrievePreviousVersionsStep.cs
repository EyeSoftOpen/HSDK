namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;

	using Newtonsoft.Json;

	public class RetrievePreviousVersionsStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		public RetrievePreviousVersionsStep(
			SolutionSystemInfo solutionSystemInfo,
			IEnumerable<string> packagesId,
			BuildAndRevision buildAndRevision)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
		}

		public CollectPackagesFromSolutionStep RetrievePreviousVersions()
		{
			var solutionFolderPath = solutionSystemInfo.FolderPath;

			var jsonsPath = Path.Combine(solutionFolderPath, "Libraries");

			var previousVersions = GetPreviousVersions(jsonsPath);

			return new CollectPackagesFromSolutionStep(
				buildAndRevision,
				solutionSystemInfo,
				packagesId,
				previousVersions);
		}

		private static IReadOnlyDictionary<string, Version> GetPreviousVersions(string jsonsPath)
		{
			var previousVersions = new Dictionary<string, Version>();

			var savedDictionaries =
				new DirectoryInfo(jsonsPath).GetFiles("*.json")
					.Select(x =>
						JsonConvert
							.DeserializeObject<IReadOnlyDictionary<string, string>>(Storage.ReadAllText(x.FullName))
							.ToDictionary(k => k.Key, v => new Version(v.Value)));

			foreach (var package in savedDictionaries.SelectMany(savedDictionary => savedDictionary))
			{
				if (previousVersions.ContainsKey(package.Key))
				{
					var dictionaryVersion = previousVersions[package.Key];

					var savedVersion = package.Value;

					if (savedVersion > dictionaryVersion)
					{
						previousVersions[package.Key] = savedVersion;
					}
				}
				else
				{
					previousVersions.Add(package.Key, package.Value);
				}
			}

			return previousVersions;
		}
	}
}