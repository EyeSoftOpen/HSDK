namespace EyeSoft.Windows.Model.ExceptionLogger
{
    using System;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Threading;
    using Core.Diagnostic;
    using Core.Logging;

    public class ExceptionHandler
	{
		private readonly Application application;

		private readonly ILogger logger;

		// ReSharper disable once NotAccessedField.Local
		private readonly bool preventLogIfDebuggerIsAttached;

		public ExceptionHandler(Application application, ILogger logger, bool preventLogIfDebuggerIsAttached)
		{
			this.application = application;
			this.logger = logger;
			this.preventLogIfDebuggerIsAttached = preventLogIfDebuggerIsAttached;
		}

		public virtual void Install()
		{
			AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainUnhandledException;
			application.DispatcherUnhandledException += OnDispatcherUnhandledException;
			TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
		}

		protected virtual void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			var aggregateException = e.Exception;
			e.SetObserved();
			LogException(aggregateException);
		}

		protected virtual void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogException((Exception)e.ExceptionObject);
		}

		protected virtual void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			e.Handled = !SystemInspector.Debugger.IsAttached;

			LogException(e.Exception);
		}

		private void LogException(Exception exception)
		{
			#if DEBUG
			if (preventLogIfDebuggerIsAttached)
			{
				if (SystemInspector.Debugger.IsAttached)
				{
					return;
				}
			}
			#endif

			logger.Error(exception);
		}
	}
}