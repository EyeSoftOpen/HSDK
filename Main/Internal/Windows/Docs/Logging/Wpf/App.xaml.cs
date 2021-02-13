namespace EyeSoft.Docs.Logging.Windows
{
    using EyeSoft.Diagnostic;
    using EyeSoft.Logging;
    using EyeSoft.Windows.Model;
    using ViewModels;

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