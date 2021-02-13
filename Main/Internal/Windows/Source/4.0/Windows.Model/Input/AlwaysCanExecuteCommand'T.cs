namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;

    public class AlwaysCanExecuteCommand<T> : AsyncRefreshCommand<T>
	{
		private readonly IViewModelChecker viewModelChecker;

		public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action<T> execute, bool isAsync = false)
			: this(viewModelChecker, viewModel, execute, null, isAsync)
		{
		}

		public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action<T> execute, Func<T, bool> canExecute, bool isAsync = false)
			: base(viewModel, execute, canExecute, isAsync)
		{
			this.viewModelChecker = viewModelChecker;
		}

		public override bool CanExecute(object parameter)
		{
			return true;
		}

		public override void Execute(object parameter)
		{
			if (!CanExecuteInternal(parameter))
			{
				viewModelChecker.Check(ViewModel);

				return;
			}

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

		private bool CanExecuteInternal(object parameter)
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
	}
}