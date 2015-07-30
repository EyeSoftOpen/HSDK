namespace EyeSoft.Nuget.Publisher.Shell
{
	using System.Collections.Generic;
	using System.Linq;

	public class PackageUpdateDataResult
	{
		public PackageUpdateDataResult(string version, IEnumerable<PackageUpdate> updates, IEnumerable<Package> packages)
		{
			Version = version;
			Updates = updates == null ? null : updates.ToArray();
			Packages = packages == null ? null : packages.ToArray();
		}

		public string Version { get; private set; }

		public IEnumerable<PackageUpdate> Updates { get; private set; }

		public IEnumerable<Package> Packages { get; private set; }
	}
}