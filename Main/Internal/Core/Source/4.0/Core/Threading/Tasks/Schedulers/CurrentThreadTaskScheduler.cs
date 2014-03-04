namespace EyeSoft.Threading.Tasks.Schedulers
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading.Tasks;

	using EyeSoft.Diagnostic;
	using EyeSoft.Logging;

	/// <summary>Provides a task scheduler that runs tasks on the current thread.</summary>
	public class CurrentThreadTaskScheduler : TaskScheduler, ILoggerTaskScheduler
	{
		/// <summary>Gets the maximum degree of parallelism for this scheduler.</summary>
		public override int MaximumConcurrencyLevel
		{
			get { return 1; }
		}

		/// <summary>Runs the provided Task synchronously on the current thread.</summary>
		/// <param name="task">The task to be executed.</param>
		protected override void QueueTask(Task task)
		{
			TryExecuteTaskWithFaultCheck(task);
		}

		/// <summary>Runs the provided Task synchronously on the current thread.</summary>
		/// <param name="task">The task to be executed.</param>
		/// <param name="taskWasPreviouslyQueued">Whether the Task was previously queued to the scheduler.</param>
		/// <returns>True if the Task was successfully executed; otherwise, false.</returns>
		protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
		{
			return TryExecuteTaskWithFaultCheck(task);
		}

		/// <summary>Gets the Tasks currently scheduled to this scheduler.</summary>
		/// <returns>An empty enumerable, as Tasks are never queued, only executed.</returns>
		protected override IEnumerable<Task> GetScheduledTasks()
		{
			return Enumerable.Empty<Task>();
		}

		protected virtual void OnTaskFault(AggregateException exception)
		{
			#if DEBUG
			if (SystemInspector.Debugger.IsAttached)
			{
				throw exception;
			}
			#endif

			Logger.Error(exception);
		}

		private bool TryExecuteTaskWithFaultCheck(Task task)
		{
			var succeded = TryExecuteTask(task);

			if (task.IsFaulted)
			{
				OnTaskFault(task.Exception);
			}

			return succeded;
		}
	}
}