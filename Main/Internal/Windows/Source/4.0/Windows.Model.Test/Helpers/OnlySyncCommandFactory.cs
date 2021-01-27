namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model.Input;
    using Model.ViewModels;

    internal class OnlySyncCommandFactory : ICommandFactory
	{
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
			return new AsyncRefreshCommand(viewModel, action);
		}

		public ICommand CreateAsync(IViewModel viewModel, Action action, Func<bool> canExecute)
		{
			return new AsyncRefreshCommand(viewModel, action, canExecute);
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
			return new AsyncRefreshCommand<T>(viewModel, action);
		}

		public ICommand CreateAsync<T>(IViewModel viewModel, Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncRefreshCommand<T>(viewModel, action, canExecute);
		}
	}
}