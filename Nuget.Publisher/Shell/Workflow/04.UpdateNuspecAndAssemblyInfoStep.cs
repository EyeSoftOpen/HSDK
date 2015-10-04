namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Core;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdateNuspecAndAssemblyInfoStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IEnumerable<string> packagesId;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public UpdateNuspecAndAssemblyInfoStep(
			BuildAndRevision buildAndRevision,
			SolutionSystemInfo solutionSystemInfo,
			IEnumerable<string> packagesId,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionSystemInfo = solutionSystemInfo;
			this.buildAndRevision = buildAndRevision;
			this.packagesId = packagesId;
			this.previousVersions = previousVersions;
		}

		public UpdatePackagesStep UpdateNuspecAndAssemblyInfo()
		{
			var checkPackages =
				new DirectoryInfo(solutionSystemInfo.FolderPath)
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

				return new UpdatePackagesStep();
			}

			var packages =
				checkPackages
					.OrderBy(x => x.Key)
					.Select(x => new PackageWithFramework(x.Key, x.Value.Versions.First(), x.Value.Packages))
					.ToArray();

			return new UpdatePackagesStep(solutionSystemInfo, buildAndRevision, packages, previousVersions);
		}
	}
}