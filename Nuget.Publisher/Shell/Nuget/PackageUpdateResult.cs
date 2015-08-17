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
	}

	public class PackageUpdateResult
	{
		public PackageUpdateResult(string id, PackageUpdateDataResult data)
		{
			Id = id;
			Data = data;
		}

		public string Id { get; }

		public PackageUpdateDataResult Data { get; }

		public override string ToString()
		{
			return Id;
		}
	}
}