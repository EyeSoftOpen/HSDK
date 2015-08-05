namespace EyeSoft.Nuget.Publisher.Shell
{
	public static class Program
	{
		public static void Main()
		{
			Hsdk
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