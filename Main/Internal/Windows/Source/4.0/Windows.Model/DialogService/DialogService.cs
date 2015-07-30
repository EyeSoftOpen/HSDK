namespace EyeSoft.Windows.Model
{
	using System;
	using System.Windows;

	public static class DialogService
	{
		private static readonly Singleton<IDialogService> singletonInstance;

		static DialogService()
		{
			singletonInstance = new Singleton<IDialogService>(() => new DefaultDialogService());
		}

		public static OpenedWindowsCollection OpenedWindows
		{
			get { return singletonInstance.Instance.OpenedWindows; }
		}

		public static void Set(Func<IDialogService> dialogService)
		{
			singletonInstance.Set(dialogService);
		}

		public static void ShowMain<TViewModel>(params object[] arguments) where TViewModel : ViewModel
		{
			singletonInstance.Instance.ShowMain<TViewModel>(arguments);
		}

		public static void ShowMain<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			singletonInstance.Instance.ShowMain(viewModel);
		}

		public static void Show<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			singletonInstance.Instance.Show(viewModel);
		}

		public static void Show<TViewModel>(params object[] arguments) where TViewModel : ViewModel
		{
			singletonInstance.Instance.Show<TViewModel>(arguments);
		}

		public static void ShowModal<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			singletonInstance.Instance.ShowModal(viewModel);
		}

		public static void ShowModal<TViewModel>(params object[] arguments) where TViewModel : ViewModel
		{
			singletonInstance.Instance.ShowModal<TViewModel>(arguments);
		}

		public static TRet ShowModal<TViewModel, TRet>(TViewModel viewModel) where TViewModel : ViewModel, IDialogViewModel<TRet>
		{
			return singletonInstance.Instance.ShowModal<TViewModel, TRet>(viewModel);
		}

		public static TRet ShowModal<TViewModel, TRet>(params object[] arguments) where TViewModel : ViewModel, IDialogViewModel<TRet>
		{
			return singletonInstance.Instance.ShowModal<TViewModel, TRet>(arguments);
		}

		public static MessageBoxResult ShowMessage(
			string title, string message, MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.Information)
		{
			return singletonInstance.Instance.ShowMessage(title, message, button, icon);
		}

		public static void Close<TViewModel>(TViewModel viewModel) where TViewModel : ViewModel
		{
			singletonInstance.Instance.Close(viewModel);
		}
	}
}