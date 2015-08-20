namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	public class CollectPackagesFromSolutionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public CollectPackagesFromSolutionStep(
			SolutionSystemInfo solutionSystemInfo,
			IEnumerable<string> packagesId,
			BuildAndRevision buildAndRevision,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
			this.previousVersions = previousVersions;
		}

		public UpdateNuspecAndAssemblyInfoStep CollectPackagesFromSolution()
		{
			return new UpdateNuspecAndAssemblyInfoStep(
				solutionSystemInfo,
				packagesId,
				buildAndRevision,
				previousVersions);
		}
	}
}