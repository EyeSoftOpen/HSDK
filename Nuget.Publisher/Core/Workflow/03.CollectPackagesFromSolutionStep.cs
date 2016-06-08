namespace EyeSoft.Nuget.Publisher.Core.Workflow
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Core.Build;
	using EyeSoft.Nuget.Publisher.Core.Core;

	public class CollectPackagesFromSolutionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly IEnumerable<string> packagesId;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IReadOnlyDictionary<string, Version> previousVersions;

		public CollectPackagesFromSolutionStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			IEnumerable<string> packagesId,
			IReadOnlyDictionary<string, Version> previousVersions)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
			this.buildAndRevision = buildAndRevision;
			this.previousVersions = previousVersions;
		}

		public UpdateNuspecAndAssemblyInfoStep CollectPackagesFromSolution()
		{
			return new UpdateNuspecAndAssemblyInfoStep(buildAndRevision, solutionSystemInfo, packagesId, previousVersions);
		}
	}
}