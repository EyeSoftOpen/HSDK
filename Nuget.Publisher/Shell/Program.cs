namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Shell.LinqPad;

	public static class Program
	{
		public static void Main()
		{
			var buildAndRevision = VersionHelper.BuildAndRevision();

			var previousVersions = HsdkPackages.GetPreviousVersions();

			HsdkPackages
				.GetAll()
				.UpdatePackages(previousVersions, buildAndRevision)
				.Dump();

			Console.ReadLine();
		}

		private static IEnumerable<PackageUpdateResult> UpdatePackages(
			this IEnumerable<PackageWithFramework> packages,
			IReadOnlyDictionary<string, string> previousVersions,
			BuildAndRevision buildAndRevision)
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

			return packageUpdateResults;
		}
	}
}