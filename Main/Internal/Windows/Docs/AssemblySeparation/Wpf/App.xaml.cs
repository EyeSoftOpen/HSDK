namespace EyeSoft.AssemblySeparation.Wpf
{
	using EyeSoft.AssemblySeparation.ViewModels;
	using EyeSoft.Windows.Model;

	public partial class App
	{
		public App()
		{
			DialogService.ShowMain<MainViewModel>();
		}
	}
}
