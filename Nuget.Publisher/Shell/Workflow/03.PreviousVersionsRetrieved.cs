namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	public class PreviousVersionsRetrieved : FluentWorkflow
	{
		private readonly string solutionFolderPath;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public PreviousVersionsRetrieved(
			string solutionFolderPath,
			IEnumerable<string> packagesId,
			BuildAndRevision buildAndRevision,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionFolderPath = solutionFolderPath;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
			this.previousVersions = previousVersions;
		}

		public PackagesFromSolutionCollected CollectPackagesFromSolution()
		{
			return new PackagesFromSolutionCollected(solutionFolderPath, packagesId, buildAndRevision, previousVersions);
		}
	}
}