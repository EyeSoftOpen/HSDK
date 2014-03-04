namespace EyeSoft.Windows.Model
{
	using System;
	using System.Windows;
	using System.Windows.Threading;

	public class SyncExecution : Execution
	{
		internal SyncExecution(Application application) : base(application, false)
		{
		}

		public T Execute<T>(Func<T> func)
		{
			return Execute(func, DispatcherPriority.Normal);
		}

		public T Execute<T>(Func<T> func, DispatcherPriority priority)
		{
			if (application == null)
			{
				return func();
			}

			var dispatcher = application.Dispatcher;

			var notNeedAccess = dispatcher.CheckAccess();

			T value;

			if (notNeedAccess)
			{
				value = func();
			}
			else
			{
				// ReSharper disable once RedundantCast
				value = (T)dispatcher.Invoke(func, priority);
			}

			return value;
		}
	}
}