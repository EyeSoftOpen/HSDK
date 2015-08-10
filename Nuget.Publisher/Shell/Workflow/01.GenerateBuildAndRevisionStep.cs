namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;

	public class GenerateBuildAndRevisionStep : FluentWorkflowStep
	{
		private readonly string solutionFolderPath;
		private readonly IEnumerable<string> packagesId;

		public GenerateBuildAndRevisionStep(string solutionFolderPath, IEnumerable<string> packagesId)
		{
			this.solutionFolderPath = solutionFolderPath;
			this.packagesId = packagesId;
		}

		public RetrievePreviousVersionsStep GenerateBuildAndRevision()
		{
			var buildAndRevision = VersionHelper.GenerateBuildAndRevision();

			return new RetrievePreviousVersionsStep(solutionFolderPath, packagesId, buildAndRevision);
		}
	}
}