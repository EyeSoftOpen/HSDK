namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncRefreshCommand<T> : BaseAsyncRefreshCommand, ICommand
    {
        private readonly Action<T> execute;

        private readonly Func<T, bool> canExecute;

        private readonly bool isAsync;

        public AsyncRefreshCommand(Action<T> execute, bool isAsync = false)
            : this(execute, null, isAsync)
        {
        }

        public AsyncRefreshCommand(Action<T> execute, Func<T, bool> canExecute, bool isAsync = false)
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
                return Task.Factory.StartNew(() => canExecute((T)parameter)).Result;
            }

            return canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            if (!isAsync)
            {
                execute((T)parameter);
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    execute((T) parameter);
                });
            }
        }
    }
}