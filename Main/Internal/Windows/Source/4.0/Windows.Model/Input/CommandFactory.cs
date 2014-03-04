namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Windows.Input;

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
			return new AsyncCommand(action, isAsync);
		}

		public ICommand CreateAsync(Action action, Func<bool> canExecute)
		{
			return new AsyncCommand(action, canExecute, isAsync);
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
			return new AsyncCommand<T>(action, isAsync);
		}

		public ICommand CreateAsync<T>(Action<T> action, Func<T, bool> canExecute)
		{
			return new AsyncCommand<T>(action, canExecute, isAsync);
		}
	}
}