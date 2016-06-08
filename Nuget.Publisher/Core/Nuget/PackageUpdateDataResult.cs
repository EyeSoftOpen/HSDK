namespace EyeSoft.Nuget.Publisher.Core.Nuget
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	public class PackageUpdateDataResult
	{
		public PackageUpdateDataResult(Version version, IEnumerable<PackageUpdate> updates, IEnumerable<Package> packages)
		{
			Version = version;
			Updates = updates?.ToArray();
			Packages = packages?.ToArray();
		}

		public Version Version { get; }

		public IEnumerable<PackageUpdate> Updates { get; }

		public IEnumerable<Package> Packages { get; }
	}
}