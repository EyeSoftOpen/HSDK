namespace EyeSoft.Windows.Model.Test.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Security;
	using System.Threading;
	using System.Threading.Tasks;

	internal class TestableTaskFactory : TaskFactory
	{
		private static readonly SynchronizationContextTaskScheduler taskScheduler =
			CreateTaskScheduler();

		public TestableTaskFactory()
			: base(taskScheduler)
		{
		}

		public void WaitAll()
		{
			taskScheduler.WaitAll();
		}

		private static SynchronizationContextTaskScheduler CreateTaskScheduler()
		{
			var synchronizationContext = new SynchronizationContext();

			var localTaskScheduler  = new SynchronizationContextTaskScheduler(synchronizationContext);

			return localTaskScheduler;
		}

		private sealed class SynchronizationContextTaskScheduler : TaskScheduler
		{
			private static readonly SendOrPostCallback postCallback = PostCallback;

			private static readonly MethodInfo executeEntryMethod =
				typeof(Task).GetMethod("ExecuteEntry", BindingFlags.Instance | BindingFlags.NonPublic);

			private readonly Queue<Task> taskQueue = new Queue<Task>();

			private readonly SynchronizationContext synchronizationContext;

			internal SynchronizationContextTaskScheduler(SynchronizationContext synchronizationContext)
			{
				if (synchronizationContext == null)
				{
					throw new InvalidOperationException("TaskScheduler FromCurrentSynchronizationContext NoCurrent");
				}

				this.synchronizationContext = synchronizationContext;
			}

			public override int MaximumConcurrencyLevel
			{
				get { return 1; }
			}

			public void WaitAll()
			{
				Task.WaitAll(taskQueue.ToArray());
			}

			[SecurityCritical]
			protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
			{
				return
					(SynchronizationContext.Current == synchronizationContext) &&
					TryExecuteTask(task);
			}

			[SecurityCritical]
			protected override IEnumerable<Task> GetScheduledTasks()
			{
				return taskQueue;
			}

			[SecurityCritical]
			protected override void QueueTask(Task task)
			{
				taskQueue.Enqueue(task);

				synchronizationContext.Post(postCallback, task);
			}

			private static void PostCallback(object obj)
			{
				executeEntryMethod.Invoke(obj, new object[] { true });
			}
		}
	}
}