namespace EyeSoft.Demo.FiscalCode.Windows
{
	using EyeSoft.Demo.FiscalCode.Windows.ViewModels;
	using EyeSoft.Windows;
	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Conventions;
    using EyeSoft.Windows.Model.DialogService;

    public partial class App
	{
		public App()
		{
			using (var mutex = this.ApplicationMutex())
			{
				if (mutex.IsAlreadyRunning)
				{
					return;
				}

				this.InstallExceptionHandler();

				////DialogService.Set(() => new DefaultDialogService(new DefaultViewModelToViewConvention(typeof(MainWindow))));

				DialogService.ShowModal<MainViewModel>();
			}
		}
	}
}