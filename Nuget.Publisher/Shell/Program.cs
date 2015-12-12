namespace EyeSoft.Nuget.Publisher.Shell
{
	using EyeSoft.Nuget.Publisher.Shell.Workflow;

	public static class Program
	{
		public static void Main()
		{
			HsdkWorkflow.GenerateBuildAndRevision()         ////  1
				.RetrievePreviousVersions()                 ////  2
				.CollectPackagesFromSolution()              ////  3
				.UpdateNuspecAndAssemblyInfo()              ////  4
				.UpdatePackages()                           ////  5
				.BuildSolution(true)                        ////  6
				.CompileNugetPackages()                     ////  7
				.PublishNugetPackages()                     ////  8
				.UpdatePreviousVersions()                   ////  9
				.DumpUpdatedPackages()                      //// 10
				.Wait();                                    //// 11
		}
	}
}