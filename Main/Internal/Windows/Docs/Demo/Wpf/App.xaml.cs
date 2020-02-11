namespace EyeSoft.Windows.Model.Demo
{
	using System.Windows;

	using EyeSoft.Logging;
	using EyeSoft.Windows.Model.Demo.Configuration;
	using EyeSoft.Windows.Model.Demo.ViewModels;

	public partial class App
	{
		protected override void OnStartup(StartupEventArgs e)
		{
			using (var applicationMutex = this.ApplicationMutex())
			{
				if (applicationMutex.IsAlreadyRunning)
				{
					return;
				}

				Current.Dispatcher.Thread.Name = "UI Thread";

				Logger.Register(new DialogLogger());
				this.InstallExceptionHandler();

				ContainerRegister.Initialize();

				DialogService.ShowModal<MainViewModel>();
			}
		}
	}
}