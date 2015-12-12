namespace EyeSoft.Architecture.Prototype.Windows.Model.ViewModels
{
    using System.Windows;

    using EyeSoft.Windows.Model;

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
            mainViewModel.ShowFlyout = true;
        }
    }
}