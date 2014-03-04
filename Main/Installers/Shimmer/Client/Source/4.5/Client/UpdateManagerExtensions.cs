namespace EyeSoft.Shimmer.Client
{
	using System.IO;

	using global::Shimmer.Client;

	public static class UpdateManagerExtensions
	{
		// ReSharper disable once UnusedParameter.Local
		public static void DeleteOldPackagesVersions(this UpdateManager updateManager)
		{
			////new DirectoryInfo(updateManager.PackageDirectory).GetFiles("*.nupkg").ToList().ForEach(f => f.Delete());
		}

		public static string LatestInstallationPath(this UpdateManager updateManager, UpdateInfo updateInfo)
		{
			var installationFolder = new DirectoryInfo(Path.Combine(updateManager.PackageDirectory, @"..\"));

			const string ApplicationPrefix = "app-";

			var version = updateInfo.FutureReleaseEntry != null ? updateInfo.FutureReleaseEntry.Version : updateInfo.CurrentlyInstalledVersion.Version;

			var latestInstallationPath = Path.Combine(installationFolder.FullName, string.Concat(ApplicationPrefix, version.ToString()));

			return latestInstallationPath;
		}
	}
}