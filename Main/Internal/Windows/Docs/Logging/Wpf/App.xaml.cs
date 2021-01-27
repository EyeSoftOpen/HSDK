namespace EyeSoft.Docs.Logging.Windows
{
    using Core.Diagnostic;
    using Core.Logging;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.DialogService;
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