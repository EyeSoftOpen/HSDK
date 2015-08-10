namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	public class CollectPackagesFromSolutionStep : FluentWorkflowStep
	{
		private readonly string solutionPath;

		private readonly string solutionFolderPath;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public CollectPackagesFromSolutionStep(
			string solutionPath,
			string solutionFolderPath,
			IEnumerable<string> packagesId,
			BuildAndRevision buildAndRevision,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionPath = solutionPath;
			this.solutionFolderPath = solutionFolderPath;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
			this.previousVersions = previousVersions;
		}

		public UpdateNuspecAndAssemblyInfoStep CollectPackagesFromSolution()
		{
			return new UpdateNuspecAndAssemblyInfoStep(
				solutionPath,
				solutionFolderPath,
				packagesId,
				buildAndRevision,
				previousVersions);
		}
	}
}