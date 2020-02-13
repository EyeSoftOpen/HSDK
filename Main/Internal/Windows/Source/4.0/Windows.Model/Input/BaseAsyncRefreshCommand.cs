namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Windows.Input;

    public abstract class BaseAsyncRefreshCommand
    {
        public BaseAsyncRefreshCommand(IViewModel viewModel)
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