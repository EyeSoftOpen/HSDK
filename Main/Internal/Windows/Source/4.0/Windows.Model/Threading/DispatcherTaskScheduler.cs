namespace EyeSoft.Windows.Model.Threading
{
	using System;
	using System.Windows;

	using EyeSoft.Diagnostic;
	using EyeSoft.Threading.Tasks.Schedulers;

	public class DispatcherTaskScheduler : LimitedConcurrencyLevelTaskScheduler
	{
		protected override void OnTaskFault(AggregateException exception)
		{
			base.OnTaskFault(exception);

			#if DEBUG
			if (SystemInspector.Debugger.IsAttached)
			{
				Application.Current.Sync().Execute(() => { throw exception; });
			}
			#endif
		}
	}
}
