namespace EyeSoft.Docs.Performance.Windows
{
    using EyeSoft.Windows.Model.DialogService;
    using ViewModels;

    public partial class App
	{
		public App()
		{
			DialogService.ShowMain<MainViewModel>();
		}
	}
}
