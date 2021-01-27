namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Threading.Tasks;
    using ViewModels;

    public class AlwaysCanExecuteCommand : AsyncRefreshCommand
	{
		private readonly IViewModelChecker viewModelChecker;

		public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action execute, bool isAsync = false)
			: this(viewModelChecker, viewModel, execute, null, isAsync)
		{
		}

		public AlwaysCanExecuteCommand(IViewModelChecker viewModelChecker, IViewModel viewModel, Action execute, Func<bool> canExecute, bool isAsync = false)
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
			if (!CanExecuteInternal())
			{
				viewModelChecker.Check(ViewModel);

				return;
			}

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

		private bool CanExecuteInternal()
		{
			if (CanExecuteFunc == null)
			{
				return true;
			}

			if (IsAsync)
			{
				return Task.Factory.StartNew(() => CanExecuteFunc()).Result;
				
			}

			return CanExecuteFunc();
		}
	}
}