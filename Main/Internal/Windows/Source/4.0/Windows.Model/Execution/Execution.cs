namespace EyeSoft.Windows.Model
{
	using System;
	using System.Windows;
	using System.Windows.Threading;

	public abstract class Execution
	{
		protected readonly Application application;

		private readonly bool isAsync;

		protected Execution(Application application, bool isAsync)
		{
			this.application = application;
			this.isAsync = isAsync;
		}

		public void Execute(Action action)
		{
			Execute(action, DispatcherPriority.Normal);
		}

		public void Execute(Delegate method, DispatcherPriority priority)
		{
			if (application == null)
			{
				ExecuteDirectlyOrDuringATest(method);
				return;
			}

			var dispatcher = application.Dispatcher;

			if ((application != null) && isAsync)
			{
				dispatcher.BeginInvoke(method);
				return;
			}

			var notRequireUiThread = dispatcher.CheckAccess();

			if (notRequireUiThread)
			{
				ExecuteDirectlyOrDuringATest(method);
				return;
			}

			if (isAsync)
			{
				dispatcher.BeginInvoke(method, priority);
				return;
			}

			dispatcher.Invoke(method, priority);
		}

		private static void ExecuteDirectlyOrDuringATest(Delegate method)
		{
            if (method is Action action)
			{
				action();
				return;
			}

			method.DynamicInvoke();
		}
	}
}