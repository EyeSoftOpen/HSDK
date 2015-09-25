using System;
using System.Windows;
using System.Windows.Threading;

namespace EyeSoft.Windows.Model.Execution
{
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
			var action = method as Action;

			if (action != null)
			{
				action();
				return;
			}

			method.DynamicInvoke();
		}
	}
}