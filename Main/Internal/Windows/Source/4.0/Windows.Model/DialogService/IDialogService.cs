namespace EyeSoft.Windows.Model
{
	using System.Windows;

	public interface IDialogService
	{
		OpenedWindowsCollection OpenedWindows { get; }

		void ShowMain<TViewModel>(params object[] arguments) where TViewModel : ViewModel;

		void ShowMain<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel;

		void Show<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel;

		void Show<TViewModel>(params object[] arguments) where TViewModel : ViewModel;

		void ShowModal<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel;

		void ShowModal<TViewModel>(params object[] arguments) where TViewModel : ViewModel;

		TRet ShowModal<TViewModel, TRet>(TViewModel viewModel)
			where TViewModel : ViewModel, IDialogViewModel<TRet>;

		TRet ShowModal<TViewModel, TRet>(params object[] arguments)
			where TViewModel : ViewModel, IDialogViewModel<TRet>;

		MessageBoxResult ShowMessage(
			string title,
			string message,
			MessageBoxButton button = MessageBoxButton.OK,
			MessageBoxImage icon = MessageBoxImage.Information);

		void Close<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel;
	}
}