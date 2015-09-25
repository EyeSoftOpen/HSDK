using EyeSoft.Windows.Model.ExceptionLogger;
using EyeSoft.Windows.Model.Execution;

namespace EyeSoft.Windows.Model
{
	using System.Diagnostics;
	using System.Windows;

	using EyeSoft.Logging;
	using EyeSoft.Threading.Tasks.Schedulers;

	public static class ApplicationExtensions
	{
		private static readonly Singleton<ExceptionHandler> singletonExceptionHandler = new Singleton<ExceptionHandler>();

		public static void InstallExceptionHandler(this Application application, bool preventLogIfDebuggerIsAttached = true)
		{
			var exceptionHandler = new ExceptionHandler(application, Logger.Instance, preventLogIfDebuggerIsAttached);
			application.InstallExceptionHandler(exceptionHandler);
		}

		public static void InstallExceptionHandler(this Application application, ExceptionHandler exceptionHandler)
		{
			const string WarningFormat =
				"If the TaskScheduler used does not implements the {0} interface," +
				" it could not intercept exceptions thrown in secondary threads.";

			Debug.WriteLine(string.Format(WarningFormat, typeof(ILoggerTaskScheduler).Name));

			singletonExceptionHandler.Set(exceptionHandler);
			exceptionHandler.Install();
		}

		public static SyncExecution Sync(this Application application)
		{
			return new SyncExecution(application);
		}

		public static AsyncExecution Async(this Application application)
		{
			return new AsyncExecution(application);
		}

		public static Window GetMainWindow(this Application application)
		{
			Window mainWindow = null;

			application.Sync().Execute(() => mainWindow = application.MainWindow);

			return mainWindow;
		}
	}
}