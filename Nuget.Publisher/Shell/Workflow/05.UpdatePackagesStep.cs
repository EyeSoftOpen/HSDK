namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdatePackagesStep : FluentWorkflowStep
	{
		private readonly string solutionPath;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IEnumerable<PackageWithFramework> packages;

		private readonly IReadOnlyDictionary<string, string> previousVersions;

		public UpdatePackagesStep(
			string solutionPath,
			BuildAndRevision buildAndRevision,
			IEnumerable<PackageWithFramework> packages,
			IReadOnlyDictionary<string, string> previousVersions)
		{
			this.solutionPath = solutionPath;
			this.buildAndRevision = buildAndRevision;
			this.packages = packages;
			this.previousVersions = previousVersions;
		}

		public UpdatePackagesStep()
		{
		}

		public BuildSolutionStep UpdatePackages()
		{
			var packageWithFrameworks = packages as PackageWithFramework[] ?? packages.ToArray();

			IReadOnlyDictionary<string, Func<Version>> packagesVersion =
				packageWithFrameworks
				.ToDictionary(k => k.Id, v => (Func<Version>)(() => new Version(v.Version)));

			var packageUpdateResults = new List<PackageUpdateResult>();

			foreach (var package in packageWithFrameworks)
			{
				var nugetDependecies = package.TryUpdateNuspecDependencies(packagesVersion).ToArray();

				var isLatestVersion = package.IsLatestVersion(previousVersions[package.Id]);

				if (isLatestVersion && !nugetDependecies.Any())
				{
					continue;
				}

				if (!isLatestVersion)
				{
					package.IncrementAssemblyInfo(buildAndRevision);
				}

				var updateDataResult = new PackageUpdateDataResult(
					package.Version,
					nugetDependecies,
					package.FrameworkPackages);

				var packageUpdate = new PackageUpdateResult(package.Id, updateDataResult);

				packageUpdateResults.Add(packageUpdate);
			}

			return new BuildSolutionStep(solutionPath, new NugetPackageResultCollection(packageUpdateResults));
		}
	}
}