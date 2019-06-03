namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncRefreshCommand : BaseAsyncRefreshCommand, ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;
        private readonly bool isAsync;

        public AsyncRefreshCommand(Action execute, bool isAsync = false)
            : this(execute, null, isAsync)
        {
        }

        public AsyncRefreshCommand(Action execute, Func<bool> canExecute, bool isAsync = false)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute;
            this.isAsync = isAsync;
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute == null)
            {
                return true;
            }

            if (isAsync)
            {
                return Task.Factory.StartNew(canExecute).Result;
            }

            return canExecute();
        }

        public void Execute(object parameter)
        {
            if (!isAsync)
            {
                execute();
                RaiseCanExecuteChanged();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    execute();
                    RaiseCanExecuteChanged();
                });
            }
        }
    }
}