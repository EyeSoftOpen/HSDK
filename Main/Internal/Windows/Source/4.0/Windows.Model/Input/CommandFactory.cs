namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;
    using ViewModels;

    public class CommandFactory : ICommandFactory
	{
		private readonly bool isAsync = true;

		public CommandFactory()
		{
		}

		public CommandFactory(bool isAsync)
		{
			this.isAsync = isAsync;
		}

		public ICommand Create(IViewModel viewModel, Action action)
		{
			return new AsyncRefreshCommand(viewModel, action);
		}

		public ICommand Create(IViewModel viewModel, Action action, Func<bool> canExecute)
		{
			return new AsyncRefreshCommand(viewModel, action, canExecute);
		}

		public ICommand CreateAsync(IViewModel viewModel, Action action)
		{
			return new AsyncRefreshCommand(viewModel, action, isAsync);
		}

		public ICommand CreateAsync(IViewModel viewModel, Action action, Func<bool> canExecute)
		{
			return new AsyncRefreshCommand(viewModel, action, canExecute, isAsync);
		}

		public ICommand Create<T>(IViewModel viewModel, Action<T> action)
		{
			return new AsyncRefreshCommand<T>(viewModel, action);
		}

		public ICommand Create<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncRefreshCommand<T>(viewModel, action, canExecute);
		}

		public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action)
		{
			return new AsyncRefreshCommand<T>(viewModel, action, isAsync);
		}

		public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncRefreshCommand<T>(viewModel, action, canExecute, isAsync);
		}
	}
}