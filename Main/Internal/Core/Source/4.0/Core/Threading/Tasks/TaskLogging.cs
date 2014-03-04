namespace EyeSoft.Threading.Tasks
{
	using System;
	using System.Reflection;
	using System.Threading.Tasks;

	using EyeSoft.Threading.Tasks.Schedulers;

	internal static class TaskLogging
	{
		private static readonly string loggerTaskSchedulerType = typeof(ILoggerTaskScheduler).Name;

		private static readonly object lockInstance = new object();

		private static readonly Singleton<TaskFactory> taskFactorySingletonInstance =
			new Singleton<TaskFactory>(() => new TaskFactory(new LimitedConcurrencyLevelTaskScheduler()));

		private static bool setted;

		public static TaskFactory Instance
		{
			get
			{
				lock (lockInstance)
				{
					var taskFactory = taskFactorySingletonInstance.Instance;

					if (setted)
					{
						return taskFactory;
					}

					SetThreading(taskFactory, false);
					setted = true;

					return taskFactory;
				}
			}
		}

		public static void SetThreading(TaskFactory taskFactory)
		{
			SetThreading(taskFactory, true);
		}

		public static void Reset()
		{
			taskFactorySingletonInstance.Reset(new TaskFactory(new CurrentThreadTaskScheduler()));
		}

		private static void SetThreading(TaskFactory taskFactory, bool setSingletonInstance)
		{
			lock (lockInstance)
			{
				var scheduler = taskFactory.Scheduler;

				if (scheduler == null)
				{
					throw new ArgumentException("The TaskScheduler of the TaskFactory cannot be null.");
				}

				if (setSingletonInstance)
				{
					taskFactorySingletonInstance.Set(taskFactory);
				}

				var loggerScheduler = scheduler as ILoggerTaskScheduler;

				if (loggerScheduler == null)
				{
					var message = string.Format("The TaskScheduler of the TaskFactory must implements the interface '{0}'.", loggerTaskSchedulerType);

					throw new ArgumentException(message);
				}

				FixTaskFactory(taskFactory, loggerScheduler);
			}
		}

		private static void FixTaskFactory(TaskFactory taskFactory, ILoggerTaskScheduler loggerScheduler)
		{
			SetScheduler(loggerScheduler);
			SetTaskFactory(taskFactory);
		}
		
		private static void SetScheduler(ILoggerTaskScheduler taskScheduler)
		{
			SetStaticField<TaskScheduler>(taskScheduler, "s_defaultTaskScheduler");
		}

		private static void SetTaskFactory(TaskFactory taskFactory)
		{
			SetStaticField<Task>(taskFactory, "s_factory");
		}

		private static void SetStaticField<T>(object instance, string fieldName)
		{
			var field = typeof(T).GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
			field.SetValue(null, instance);
		}
	}
}