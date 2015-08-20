namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	using EyeSoft.Nuget.Publisher.Shell.Build;

	public class GenerateBuildAndRevisionStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;
		private readonly IEnumerable<string> packagesId;

		public GenerateBuildAndRevisionStep(SolutionSystemInfo solutionSystemInfo, IEnumerable<string> packagesId)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.packagesId = packagesId;
		}

		public RetrievePreviousVersionsStep GenerateBuildAndRevision()
		{
			var buildAndRevision = VersionHelper.GenerateBuildAndRevision();

			return new RetrievePreviousVersionsStep(solutionSystemInfo, packagesId, buildAndRevision);
		}
	}
}