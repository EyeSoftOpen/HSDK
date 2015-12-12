namespace EyeSoft.Nuget.Publisher.Shell.Nuget
{
	using System;

	using global::System.Collections.Generic;
	using global::System.Linq;

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