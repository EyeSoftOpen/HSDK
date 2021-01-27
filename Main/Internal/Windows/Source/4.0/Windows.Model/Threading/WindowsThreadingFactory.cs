namespace EyeSoft.Windows.Model.Threading
{
	using System.Threading;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Threading;
    using Core.Threading.Tasks;

    public class WindowsThreadingFactory
	{
		static WindowsThreadingFactory()
		{
			VerifySynchronizationContext();
		}

		public static TaskFactory Instance
		{
			get { return SetForMultithreadingTaskScheduler(); }
		}

		public static TaskFactory SetMode(bool multithread)
		{
			return multithread ? SetForMultithreadingTaskScheduler() : SetCurrentThreadScheduler();
		}

		public static void Set(TaskFactory taskFactory)
		{
			ThreadingFactory.Set(taskFactory);
		}

		public static TaskFactory SetForMultithreadingTaskScheduler()
		{
			return ThreadingFactory.Instance;
		}

		public static TaskFactory SetCurrentThreadScheduler()
		{
			return ThreadingFactory.SetCurrentThreadScheduler();
		}

		public static TaskScheduler FromCurrentSynchronizationContext()
		{
			VerifySynchronizationContext();

			return TaskScheduler.FromCurrentSynchronizationContext();
		}

		private static void VerifySynchronizationContext()
		{
			if (SynchronizationContext.Current != null)
			{
				return;
			}

			if (IsRunningInTest())
			{
				SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				return;
			}

			var context = new DispatcherSynchronizationContext(Application.Current.Dispatcher);
			SynchronizationContext.SetSynchronizationContext(context);
		}

		private static bool IsRunningInTest()
		{
			return Application.Current == null;
		}
	}
}