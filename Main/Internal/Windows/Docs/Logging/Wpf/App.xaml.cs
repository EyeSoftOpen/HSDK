namespace EyeSoft.Docs.Logging.Wpf
{
	using EyeSoft.Diagnostic;
	using EyeSoft.Docs.Logging.Wpf.ViewModels;
	using EyeSoft.Logging;
	using EyeSoft.Windows.Model;

	public partial class App
	{
		public App()
		{
			if (SystemInspector.Debugger.IsAttached)
			{
				DialogService.ShowMessage("Debugger attached", "To test this application launch without debugging.");
				Shutdown();
				return;
			}

			var mainViewModel = new MainViewModel();

			Logger.Register(new ViewLogger(mainViewModel));

			this.InstallExceptionHandler();

			DialogService.ShowMain(mainViewModel);
		}
	}
}