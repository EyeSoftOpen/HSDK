namespace EyeSoft.Docs.Performance.Wpf
{
	using EyeSoft.Docs.Performance.Wpf.ViewModels;
	using EyeSoft.Windows.Model;

	public partial class App
	{
		public App()
		{
			DialogService.ShowMain<MainViewModel>();
		}
	}
}
