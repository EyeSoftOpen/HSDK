namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public abstract class BaseAsyncRefreshCommand
    {
        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;

            var dispatcher = Application.Current?.Dispatcher;

            void Raise() => handler?.Invoke(this, new EventArgs());

            if (dispatcher != null && !dispatcher.CheckAccess())
            {
                dispatcher.Invoke(Raise);
            }
            else
            {
                Raise();
            }

            CommandManager.InvalidateRequerySuggested();
        }
    }
}