namespace EyeSoft.Demo.Navigation.Windows.Presentation.Startup
{
	using System;
	using System.IO;
	using System.Linq;
	using System.Windows;

	using Exceptionless;

	using EyeSoft.IO;
	using EyeSoft.Windows;
	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Runtime;

	using MessageBox = System.Windows.MessageBox;

	public class Program
	{
		[STAThread]
		public static void Main()
		{
			try
			{
				var application = new Application { ShutdownMode = ShutdownMode.OnMainWindowClose };

				using (var applicationMutex = application.ApplicationMutex())
				{
					if (applicationMutex.IsAlreadyRunning)
					{
						return;
					}

					ExceptionlessClient.Current.Register();

					application.InstallExceptionHandler();

					////SystemInspector.Debugger.SetAsDetached();

					var debugOrInstalledPath =
						ApplicationRuntime.DebugOrRuntimePath(
							"EyeSoft",
							"NavigationDemo",
							@"Internal\Windows\Docs\Navigation\Windows\");

					CopyDebugToRuntimePath();

					application.Start(debugOrInstalledPath, "EyeSoft.Demo.Navigation.Windows.Presentation");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString(), "Error on the application");
			}
		}

		private static void CopyDebugToRuntimePath()
		{
			var runtimePath = ApplicationRuntime.RuntimePath("EyeSoft", "NavigationDemo");
			var debugPath = ApplicationRuntime.DebugPath(@"Internal\Windows\Docs\Navigation\Windows\");
			
			Storage.Directory(runtimePath).Create();
			Storage.GetFiles(debugPath, "*.*").ToList().ForEach(f => f.CopyTo(Path.Combine(runtimePath, f.Name), true));
		}

		////private static string InstallUsingShimmer()
		////{
		////	var shimmerConfiguration =
		////		new ShimmerConfiguration("Navigation.Demo", "navigationdemo.eye-soft.com", @"Internal\Wpf.Facilities\Docs\Navigation\Windows\");

		////	var debugOrInstalledPath = shimmerConfiguration.UrlOrPath;
		////		ShimmerInstaller.Install(shimmerConfiguration, OnUpdatesAvailable);
			
		////	return debugOrInstalledPath;
		////}

		////private static void OnUpdatesAvailable(ShimmerInstaller shimmerInstaller)
		////{
		////	////var mainWindow = new Main(shimmerInstaller);
		////	////mainWindow.ShowDialog();
		////}
	}
}