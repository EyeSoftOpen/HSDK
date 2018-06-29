namespace EyeSoft.Windows.Installer.InstallMate
{
	using System;
	using System.Diagnostics;
	using System.Reflection;
	using System.Windows;

	using EyeSoft.Windows.Runtime;

	public class InstallMateInstaller : IDisposable
	{
		private readonly Window window;

		private readonly IntPtr handle;

		private readonly bool updateAvailable;

		private readonly Assembly entryAssemblyToRestart;

		public InstallMateInstaller(
			ApplicationRuntimeConfiguration applicationConfiguration,
			Func<Window> createDialog,
			Assembly entryAssemblyToRestart = null)
		{
			this.entryAssemblyToRestart = entryAssemblyToRestart ?? Assembly.GetEntryAssembly();

			const string DownloadFormatUrl = @"http://{0}/{1}.Setup.txt";

			var applicationName = applicationConfiguration.ApplicationName;

			var downloadUrl = applicationConfiguration.DownloadUrl;

			var setupUrl = string.Format(DownloadFormatUrl, downloadUrl, applicationName);
			window = createDialog();

			Execute("Clean", ApplicationUpdater.CleanupUpdate);

			handle = ApplicationUpdater.OpenUpdate(IntPtr.Zero);
			ApplicationUpdater.CheckUpdate(handle, setupUrl, string.Empty, 0, out updateAvailable);

			DebugOrRuntimePath = applicationConfiguration.DebugOrRuntimePath;
		}

		public bool UpdateAvailable
		{
			get { return updateAvailable; }
		}

		public string DebugOrRuntimePath { get; private set; }

		public void InstallUpdate(bool modal = false)
		{
			if (modal)
			{
				window.ShowDialog();
			}
			else
			{
				window.Show();
			}

			Execute("Downloading", () => ApplicationUpdater.DownloadUpdate(handle, 1));
			
			window.Hide();

			Execute("Install", () => ApplicationUpdater.InstallUpdate(handle, 0));
		}

		public void Dispose()
		{
			Execute("Close", () => ApplicationUpdater.CloseUpdate(handle));
		}

	    public void RestartApplication()
		{
			Process.Start(entryAssemblyToRestart.Location);

			if (Application.Current == null)
			{
				return;
			}

			Application.Current.Shutdown();
		}

		private void Execute(string message, Action action)
		{
			Debug.WriteLine(message);
			action();
		}
	}
}