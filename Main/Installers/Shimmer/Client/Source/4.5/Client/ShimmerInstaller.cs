namespace EyeSoft.Shimmer.Client
{
	using System;
	using System.Linq;
	using System.Reactive.Linq;

	using EyeSoft.Diagnostic;
	using EyeSoft.Reflection;

	using global::Shimmer.Client;

	public class ShimmerInstaller : IDisposable
	{
		private readonly UpdateManager updateManager;

		private readonly UpdateInfo updateInfo;

		public ShimmerInstaller(string applicationName, string urlOrPath, string projectName, FrameworkVersion frameworkVersion = FrameworkVersion.Net45)
			: this(new ShimmerConfiguration(applicationName, urlOrPath, projectName, frameworkVersion))
		{
		}

		public ShimmerInstaller(ShimmerConfiguration configuration)
		{
			var urlOrPath = configuration.UrlOrPath;
			var applicationName = configuration.ApplicationName;
			var frameworkVersion = configuration.FrameworkVersion;

			updateManager = new UpdateManager(urlOrPath, applicationName, frameworkVersion);
			updateInfo = updateManager.CheckForUpdate().Wait();

			UpdatesAvailable = updateInfo.ReleasesToApply.Any();
			InstallationPath = updateManager.LatestInstallationPath(updateInfo);
		}

		public string InstallationPath { get; private set; }

		public bool UpdatesAvailable { get; private set; }

		public static string Install(ShimmerConfiguration shimmerConfiguration, Action<ShimmerInstaller> onUpdatesAvailable)
		{
			using (var shimmerInstaller = new ShimmerInstaller(shimmerConfiguration))
			{
				var debugOrInstalledPath = shimmerInstaller.InstallationPath;

				if (SystemInspector.Debugger.IsAttached)
				{
					debugOrInstalledPath = shimmerConfiguration.DebugOrInstalledPath();
				}

				AssembliesResolver.AppendResolver(new FolderAssemblyResolver(debugOrInstalledPath));

				if (shimmerInstaller.UpdatesAvailable)
				{
					onUpdatesAvailable(shimmerInstaller);
				}

				return debugOrInstalledPath;
			}
		}

		public void InstallOrUpdate()
		{
			if (UpdatesAvailable)
			{
				DownloadAndApplyUpdates();
			}

			updateManager.DeleteOldPackagesVersions();
		}

		public void Dispose()
		{
			updateManager.Dispose();
		}

		private void DownloadAndApplyUpdates()
		{
			updateManager.DownloadReleasesAsync(updateInfo.ReleasesToApply, x => { }).Wait();

			updateManager.ApplyReleasesAsync(updateInfo, x => { }).Wait();
		}
	}
}