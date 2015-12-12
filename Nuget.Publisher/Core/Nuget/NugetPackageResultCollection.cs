namespace EyeSoft.Nuget.Publisher.Shell.Nuget
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class NugetPackageResultCollection
	{
		private readonly IEnumerable<PackageUpdateResult> packageUpdateResults;

		public NugetPackageResultCollection(IEnumerable<PackageUpdateResult> packageUpdateResults)
		{
			this.packageUpdateResults = packageUpdateResults;
		}

		public IEnumerable<Package> NugetPackages
		{
			get { return packageUpdateResults.SelectMany(x => x.Data.Packages.Where(y => y.HasNuget)); }
		}

		public IReadOnlyDictionary<string, Version> NewPackagesVersions
		{
			get { return packageUpdateResults.ToDictionary(k => k.Id, v => v.Data.Version); }
		}
	}
}