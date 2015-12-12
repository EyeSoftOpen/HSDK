namespace EyeSoft.Nuget.Publisher.Shell.Workflow
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.Build;
	using EyeSoft.Nuget.Publisher.Shell.Core;
	using EyeSoft.Nuget.Publisher.Shell.Nuget;

	public class UpdatePackagesStep : FluentWorkflowStep
	{
		private readonly SolutionSystemInfo solutionSystemInfo;

		private readonly BuildAndRevision buildAndRevision;

		private readonly IEnumerable<PackageWithFramework> packages;

		private readonly IReadOnlyDictionary<string, Version> previousVersions;

		public UpdatePackagesStep(
			SolutionSystemInfo solutionSystemInfo,
			BuildAndRevision buildAndRevision,
			IEnumerable<PackageWithFramework> packages,
			IReadOnlyDictionary<string, Version> previousVersions)
		{
			this.solutionSystemInfo = solutionSystemInfo;
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

			packageWithFrameworks =
				packageWithFrameworks.OrderBy(x => x.IsLatestVersion(previousVersions[x.Id])).ToArray();

			var packageUpdateResults = new List<PackageUpdateResult>();

			foreach (var package in packageWithFrameworks)
			{
				var isLatestVersion = package.IsLatestVersion(previousVersions[package.Id]);

				if (isLatestVersion)
				{
					continue;
				}

				package.IncrementAssemblyInfo(buildAndRevision);
			}

			IReadOnlyDictionary<string, Func<Version>> packagesVersion =
				packageWithFrameworks
				.ToDictionary(k => k.Id, v => (Func<Version>)(() => v.Version));

			foreach (var package in packageWithFrameworks)
			{
				var nugetDependecies = package.TryUpdateNuspecDependencies(packagesVersion).ToArray();

				var isLatestVersion = package.IsLatestVersion(previousVersions[package.Id]);

				if (!nugetDependecies.Any() && isLatestVersion)
				{
					continue;
				}

				package.UpdateNuspecVersion();

				var updateDataResult = new PackageUpdateDataResult(
					package.Version,
					nugetDependecies,
					package.FrameworkPackages);

				var packageUpdate = new PackageUpdateResult(package.Id, updateDataResult);

				packageUpdateResults.Add(packageUpdate);
			}

			return new BuildSolutionStep(buildAndRevision, solutionSystemInfo, new NugetPackageResultCollection(packageUpdateResults));
		}
	}
}