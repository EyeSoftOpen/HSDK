namespace EyeSoft.Demo.Validation.Windows
{
	using System.Windows;
    using EyeSoft.Logging;
    using EyeSoft.Demo.Validation.Windows.ViewModels;

    public partial class App
	{
		private readonly MainViewModel mainViewModel;

		public App()
		{
			//Logger.Register(new FileLogger(@"C:\Temp\Test"));

			Logger.Register(new ConsoleLogger());
			mainViewModel = new MainViewModel();
			Current.MainWindow = new Main { DataContext = mainViewModel };
			Current.MainWindow.Show();
		}

		protected override void OnExit(ExitEventArgs e)
		{
			mainViewModel?.Dispose();
			base.OnExit(e);
		}
	}
}
