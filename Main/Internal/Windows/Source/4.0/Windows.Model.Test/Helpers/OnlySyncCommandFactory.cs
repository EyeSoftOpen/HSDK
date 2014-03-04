namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Windows.Input;

	using EyeSoft.Windows.Model.Input;

	internal class OnlySyncCommandFactory : ICommandFactory
	{
		public ICommand Create(Action action)
		{
			return new AsyncCommand(action);
		}

		public ICommand Create(Action action, Func<bool> canExecute)
		{
			return new AsyncCommand(action, canExecute);
		}

		public ICommand CreateAsync(Action action)
		{
			return new AsyncCommand(action);
		}

		public ICommand CreateAsync(Action action, Func<bool> canExecute)
		{
			return new AsyncCommand(action, canExecute);
		}

		public ICommand Create<T>(Action<T> action)
		{
			return new AsyncCommand<T>(action);
		}

		public ICommand Create<T>(Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncCommand<T>(action, canExecute);
		}

		public ICommand CreateAsync<T>(Action<T> action)
		{
			return new AsyncCommand<T>(action);
		}

		public ICommand CreateAsync<T>(Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncCommand<T>(action, canExecute);
		}
	}
}