namespace EyeSoft.Demo.FiscalCode.Wpf
{
	using EyeSoft.Demo.FiscalCode.Wpf.ViewModels;
	using EyeSoft.Windows;
	using EyeSoft.Windows.Model;

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

				DialogService.ShowModal<MainViewModel>();
			}
		}
	}
}