namespace EyeSoft.Nuget.Publisher.Core.Nuget
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Nuget.Publisher.Core.Core;

	public class PackageWithFramework
	{
		public PackageWithFramework(string id, Version version, IEnumerable<Package> frameworkPackages)
		{
			Id = id;
			Version = version;
			FrameworkPackages = frameworkPackages.ToArray();
		}

		public string Id { get; }

		public Version Version { get; private set; }

		public IEnumerable<Package> FrameworkPackages { get; }

		public override string ToString()
		{
			return $"{Id} {Version}";
		}

		public IEnumerable<PackageUpdate> TryUpdateNuspecDependencies(
			IReadOnlyDictionary<string, Func<Version>> packagesVersion)
		{
			var nugetPackageReference = FrameworkPackages.Single(x => x.HasNuget);

			var updatedNuspecDependencies = nugetPackageReference.TryUpdateNuspecDependencies(packagesVersion);

			return updatedNuspecDependencies;
		}

		public void IncrementAssemblyInfo(BuildAndRevision buildAndRevision)
		{
			var newVersion = Version.Increment(buildAndRevision);

			foreach (var frameworkPackage in FrameworkPackages)
			{
				frameworkPackage.IncrementAssemblyInfo(newVersion);
			}

			Version = newVersion;
		}

		public bool IsLatestVersion(Version previousVersion)
		{
			var isLatestVersion = previousVersion == Version;

			return isLatestVersion;
		}

		internal void UpdateNuspecVersion()
		{
			var nugetPackageReference = FrameworkPackages.Single(x => x.HasNuget);

			nugetPackageReference.UpdateNuspecVersion(Version);
		}
	}
}