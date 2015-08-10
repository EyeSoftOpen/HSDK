namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public static class Program
	{
		public static void Main()
		{
			new HsdkWorkflow()
				.GenerateBuildAndRevision()         //  1
				.RetrievePreviousVersions()         //  2
				.CollectPackagesFromSolution()      //  3
				.UpdateNuspecAndAssemblyInfo()      //  4
				.UpdatePackages()                   //  5
				.BuildSolution()                    //  6
				.CompileNugetPackages()				//  7
				.PublishNugetPackages()             //  8
				.UpdatePreviousVersions()           //  9
				.DumpUpdatedPackages()              // 10
				.Wait();                            // 11
		}
	}
}