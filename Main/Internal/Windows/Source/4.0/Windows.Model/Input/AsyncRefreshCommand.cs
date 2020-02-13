namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;

    public class AsyncRefreshCommand : BaseAsyncRefreshCommand, ICommand
    {
        public AsyncRefreshCommand(IViewModel viewModel, Action execute, bool isAsync = false)
            : this(viewModel, execute, null, isAsync)
        {
        }

        public AsyncRefreshCommand(IViewModel viewModel, Action execute, Func<bool> canExecute, bool isAsync = false)
            : base(viewModel)
        {
            ExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
            CanExecuteFunc = canExecute;
            IsAsync = isAsync;
        }

        protected Action ExecuteAction { get; private set; }

        protected Func<bool> CanExecuteFunc { get; private set; }

        protected bool IsAsync { get; private set; }

        public virtual bool CanExecute(object parameter)
        {
            if (CanExecuteFunc == null)
            {
                return true;
            }

            if (IsAsync)
            {
                return Task.Factory.StartNew(CanExecuteFunc).Result;
            }

            return CanExecuteFunc();
        }

        public virtual void Execute(object parameter)
        {
            if (!IsAsync)
            {
                ExecuteAction();
            }
            else
            {
                Task.Factory.StartNew(() =>
                {
                    ExecuteAction();
                });
            }
        }
    }
}