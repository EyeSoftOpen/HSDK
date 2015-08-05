namespace EyeSoft.Nuget.Publisher.Shell.Nuget
{
	using global::System.Collections.Generic;
	using global::System.Linq;

	public class PackageUpdateDataResult
	{
		public PackageUpdateDataResult(string version, IEnumerable<PackageUpdate> updates, IEnumerable<Package> packages)
		{
			Version = version;
			Updates = updates?.ToArray();
			Packages = packages?.ToArray();
		}

		public string Version { get; }

		public IEnumerable<PackageUpdate> Updates { get; }

		public IEnumerable<Package> Packages { get; }
	}
}