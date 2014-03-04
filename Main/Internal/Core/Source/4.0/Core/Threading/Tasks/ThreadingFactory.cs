namespace EyeSoft.Threading.Tasks
{
	using System.Threading.Tasks;

	using EyeSoft.Threading.Tasks.Schedulers;

	public static class ThreadingFactory
	{
		public static TaskFactory Instance
		{
			get { return TaskLogging.Instance; }
		}

		public static TaskFactory SetCurrentThreadScheduler()
		{
			var taskFactory = new TaskFactory(new CurrentThreadTaskScheduler());
			Set(taskFactory);
			return taskFactory;
		}

		public static void Set(TaskScheduler taskScheduler)
		{
			SetOnTaskFactory(new TaskFactory(taskScheduler));
		}

		public static void Set(TaskFactory taskFactory)
		{
			SetOnTaskFactory(taskFactory);
		}

		public static void Reset()
		{
			TaskLogging.Reset();
		}

		private static void SetOnTaskFactory(TaskFactory taskFactory)
		{
			TaskLogging.SetThreading(taskFactory);
		}
	}
}