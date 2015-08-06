namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public static class Program
	{
		public static void Main()
		{
			new HsdkWorkflow()
				.GenerateBuildAndRevision()
				.GetPreviousVersions()
				.CollectPackagesFromSolution()
				.UpdateNuspecAndAssemblyInfo()
				.UpdatePackages()
				.BuildSolution()
				.PublishNugetPackages()
				.UpdatePreviousVersions()
				.DumpUpdatedPackages()
				.Wait();
		}
	}
}