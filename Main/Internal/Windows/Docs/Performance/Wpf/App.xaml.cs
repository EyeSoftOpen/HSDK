namespace EyeSoft.Docs.Performance.Windows
{
    using EyeSoft.Windows.Model;
    using ViewModels;

    public partial class App
	{
		public App()
		{
			DialogService.ShowMain<MainViewModel>();
		}
	}
}
