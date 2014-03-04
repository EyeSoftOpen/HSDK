namespace EyeSoft.Windows.Model.Input
{
	using System;
	using System.Diagnostics;
	using System.Threading.Tasks;
	using System.Windows.Input;

	public class AsyncCommand<T> : ICommand
	{
		private readonly Action<T> execute;

		private readonly Func<T, bool> canExecute;

		private readonly bool isAsync;

		public AsyncCommand(Action<T> execute, bool isAsync = false)
			: this(execute, null, isAsync)
		{
		}

		public AsyncCommand(Action<T> execute, Func<T, bool> canExecute, bool isAsync = false)
		{
			if (execute == null)
			{
				throw new ArgumentNullException("execute");
			}

			this.execute = execute;
			this.canExecute = canExecute;
			this.isAsync = isAsync;
		}

		#if SILVERLIGHT
		public event EventHandler CanExecuteChanged;
		#endif

		#if !SILVERLIGHT
		public event EventHandler CanExecuteChanged
		{
			add
			{
				if (canExecute != null)
				{
					CommandManager.RequerySuggested += value;
				}
			}

			remove
			{
				if (canExecute != null)
				{
					CommandManager.RequerySuggested -= value;
				}
			}
		}
		#endif

		public void RaiseCanExecuteChanged()
		{
			#if SILVERLIGHT
			var handler = CanExecuteChanged;
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
			#else
			CommandManager.InvalidateRequerySuggested();
			#endif
		}

		[DebuggerStepThrough]
		public bool CanExecute(object parameter)
		{
			if (canExecute == null)
			{
				return true;
			}

			return !isAsync ? canExecute((T)parameter) : Task.Factory.StartNew(() => canExecute((T)parameter)).Result;
		}

		public void Execute(object parameter)
		{
			Action action = ()  => execute((T)parameter);

			if (!isAsync)
			{
				action();
				return;
			}

			Task.Factory.StartNew(action);
		}
	}
}