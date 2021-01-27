namespace EyeSoft.Docs.Logging.Windows.ViewModels
{
    using System;
    using System.Windows.Input;
    using EyeSoft.Windows.Model.Collections.ObjectModel;
    using EyeSoft.Windows.Model.ViewModels;

    public class MainViewModel : AutoRegisterViewModel
	{
		private readonly IObservableCollection<ExceptionViewModel> exceptions;

		public MainViewModel()
		{
			exceptions = CollectionFactory.Create<ExceptionViewModel>();
		}

		public ICommand UiExceptionCommand { get; private set; }

		public ICommand ThreadExceptionCommand { get; private set; }

		public IReadOnlyObservableCollection<ExceptionViewModel> Exceptions => exceptions;

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