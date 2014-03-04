namespace EyeSoft.Docs.Logging.Wpf.ViewModels
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model;
	using EyeSoft.Windows.Model.Collections.ObjectModel;

	public class MainViewModel : AutoRegisterViewModel
	{
		private readonly IObservableCollection<ExceptionViewModel> exceptions;

		public MainViewModel()
		{
			exceptions = CollectionFactory.Create<ExceptionViewModel>();
		}

		public ICommand UiExceptionCommand { get; private set; }

		public ICommand ThreadExceptionCommand { get; private set; }

		public IReadOnlyObservableCollection<ExceptionViewModel> Exceptions
		{
			get { return exceptions; }
		}

		internal void LogError(string message, DateTime dateTime)
		{
			exceptions.Insert(0, new ExceptionViewModel(message.Replace("\r\n", " "), dateTime));
		}

		protected void SyncUiException()
		{
			throw new Exception("UI exception.");
		}

		protected void ThreadException()
		{
			throw new Exception("Thread exception.");
		}
	}
}