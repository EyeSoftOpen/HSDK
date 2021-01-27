namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Windows.Input;
    using ViewModels;

    public abstract class BaseAsyncRefreshCommand
    {
        protected BaseAsyncRefreshCommand(IViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        protected IViewModel ViewModel { get; }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}