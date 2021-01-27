namespace EyeSoft.Windows.Model.Input
{
    using System;
    using System.Windows.Input;
    using ViewModels;

    public class AlwaysCanExecuteCommandFactory : ICommandFactory
	{
		private readonly bool isAsync = true;
		private readonly IViewModelChecker viewModelChecker;

		public AlwaysCanExecuteCommandFactory(bool isAsync, IViewModelChecker viewModelChecker)
		{
			this.isAsync = isAsync;
			this.viewModelChecker = viewModelChecker;
		}

		public ICommand Create(IViewModel viewModel, Action action)
		{
			return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action);
		}

		public ICommand Create(IViewModel viewModel, Action action, Func<bool> canExecute)
		{
			return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, canExecute);
		}

		public ICommand CreateAsync(IViewModel viewModel, Action action)
		{
			return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, isAsync);
		}

		public ICommand CreateAsync(IViewModel viewModel, Action action, Func<bool> canExecute)
		{
			return new AlwaysCanExecuteCommand(viewModelChecker, viewModel, action, canExecute, isAsync);
		}

		public ICommand Create<T>(IViewModel viewModel, Action<T> action)
		{
			return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action);
		}

		public ICommand Create<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
		{
			return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, canExecute);
		}

		public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action)
		{
			return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, isAsync);
		}

		public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
		{
			return new AlwaysCanExecuteCommand<T>(viewModelChecker, viewModel, action, canExecute, isAsync);
		}
	}
}