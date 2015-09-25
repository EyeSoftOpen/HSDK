namespace EyeSoft.Architecture.Prototype.Windows.Model
{
	using System.Windows;

	using EyeSoft.Architecture.Prototype.Windows.Model.ViewModels;
	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.ViewModels;

	public static class FlyoutService
	{
		public static void Show<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			Application.Current.Sync().Execute(() => ShowFlyout(viewModel));
		}

		private static void ShowFlyout<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			var mainViewModel = Application.Current.MainWindow.DataContext as FlyoutShellViewModel;

			if (mainViewModel == null)
			{
				return;
			}

			mainViewModel.FlyoutDataContext = viewModel;
			mainViewModel.IsFlyoutOpen = true;
		}
	}
}