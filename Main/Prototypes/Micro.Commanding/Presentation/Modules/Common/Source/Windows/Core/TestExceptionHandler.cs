namespace Windows
{
	using System;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Threading;

	using EyeSoft.Logging;
	using EyeSoft.Windows.Model;

	public class TestExceptionHandler : ExceptionHandler
	{
		private readonly ILogger logger;

		public TestExceptionHandler(Application application, ILogger logger, bool preventLogIfDebuggerIsAttached)
			: base(application, logger, preventLogIfDebuggerIsAttached)
		{
			this.logger = logger;
		}

		protected override void OnCurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			logger.Error((Exception)e.ExceptionObject);
			//base.OnCurrentDomainUnhandledException(sender, e);
		}

		protected override void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
		{
			logger.Error(e.Exception);
			e.Handled = true;

			base.OnDispatcherUnhandledException(sender, e);
		}

		protected override void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			logger.Error(e.Exception);
			e.SetObserved();

			base.OnUnobservedTaskException(sender, e);
		}
	}
}