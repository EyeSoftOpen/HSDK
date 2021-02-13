namespace EyeSoft.Demo.Navigation.Windows.Presentation
{
	using EyeSoft.Demo.Navigation.Windows.ViewModels;
	using EyeSoft.Windows;
	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Conventions;

    internal class ApplicationEntryPoint : IApplicationEntryPoint
	{
		public void Start()
		{
			DialogService.Set(() => new DefaultDialogService(new DefaultViewModelToViewConvention(typeof(ApplicationEntryPoint).Assembly)));

			DialogService.ShowMain<MainViewModel>();
		}
	}
}