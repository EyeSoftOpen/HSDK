namespace EyeSoft.AssemblySeparation.Windows
{
	using EyeSoft.AssemblySeparation.ViewModels;
	using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.DialogService;

    public partial class App
	{
		public App()
		{
			DialogService.ShowMain<MainViewModel>();
		}
	}
}
