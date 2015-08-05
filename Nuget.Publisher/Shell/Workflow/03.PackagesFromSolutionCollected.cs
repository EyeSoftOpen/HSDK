namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.LinqPad;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class PackagesFromSolutionCollected : FluentWorkflow
	{
		private readonly string solutionPath;

		private readonly string solutionFolderPath;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IEnumerable<string> packagesId;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public PackagesFromSolutionCollected(
			string solutionPath,
			string solutionFolderPath,
			IEnumerable<string> packagesId,
			BuildAndRevision buildAndRevision,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionPath = solutionPath;
			this.solutionFolderPath = solutionFolderPath;
			this.buildAndRevision = buildAndRevision;
			this.packagesId = packagesId;
			this.previousVersions = previousVersions;
		}

		public NuspecAndAssemblyInfoUpdated UpdateNuspecAndAssemblyInfo()
		{
			var checkPackages =
				new DirectoryInfo(solutionFolderPath)
					.GetFiles("AssemblyInfo.cs", SearchOption.AllDirectories)
					.Select(Packages.Parse)
					.Where(x => x != null && packagesId.Contains(x.Title))
					.GroupBy(x => x.Title)
					.ToDictionary(k => k.Key, v => new { Versions = v.Select(y => y.PackageVersion), Packages = v.Select(y => y) })
					.Where(x => x.Value.Packages.Any(y => y.HasNuget))
					.ToArray();

			var packagesWithDifferentVersions = checkPackages.Where(x => x.Value.Versions.Distinct().Count() > 1).ToArray();

			if (packagesWithDifferentVersions.Any())
			{
				ConsoleHelper.WriteLine("Packages with different versions to be fixed");

				return new NuspecAndAssemblyInfoUpdated();
			}

			var packages =
				checkPackages
					.OrderBy(x => x.Key)
					.Select(x => new PackageWithFramework(x.Key, x.Value.Versions.First(), x.Value.Packages))
					.ToArray();

			return new NuspecAndAssemblyInfoUpdated(solutionPath, buildAndRevision, packages, previousVersions);
		}
	}
}