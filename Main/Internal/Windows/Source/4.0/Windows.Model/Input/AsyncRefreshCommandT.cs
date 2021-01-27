namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using ViewModels;

    public class AsyncRefreshCommand<T> : BaseAsyncRefreshCommand, ICommand
	{
		public AsyncRefreshCommand(IViewModel viewModel, Action<T> execute, bool isAsync = false)
			: this(viewModel, execute, null, isAsync)
		{
		}

		public AsyncRefreshCommand(IViewModel viewModel, Action<T> execute, Func<T, bool> canExecute, bool isAsync = false)
			: base(viewModel)
		{
			ExecuteAction = execute ?? throw new ArgumentNullException(nameof(execute));
			CanExecuteFunc = canExecute;
			IsAsync = isAsync;
		}

		protected Action<T> ExecuteAction { get; private set; }

		protected Func<T, bool> CanExecuteFunc { get; private set; }

		protected bool IsAsync { get; private set; }

		public virtual bool CanExecute(object parameter)
		{
			if (CanExecuteFunc == null)
			{
				return true;
			}

			if (IsAsync)
			{
				return Task.Factory.StartNew(() => CanExecuteFunc((T)parameter)).Result;
			}

			return CanExecuteFunc((T)parameter);
		}

		public virtual void Execute(object parameter)
		{
			if (!IsAsync)
			{
				ExecuteAction((T)parameter);
			}
			else
			{
				Task.Factory.StartNew(() =>
				{
					ExecuteAction((T)parameter);
				});
			}
		}
	}
}