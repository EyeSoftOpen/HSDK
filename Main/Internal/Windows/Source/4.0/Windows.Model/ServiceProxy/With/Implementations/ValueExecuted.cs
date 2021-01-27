namespace EyeSoft.Windows.Model.ServiceProxy.With.Implementations
{
    using System;
    using System.Threading.Tasks;
    using Collections.ObjectModel;
    using Threading;

    internal class ValueExecuted<TParameter> : IValueExecuted<TParameter>
	{
		private readonly Task<TParameter> task;

		private readonly TParameter value;

		public ValueExecuted(TParameter value)
		{
			this.value = value;
		}

		public ValueExecuted(Task<TParameter> task, TParameter value) : this(value)
		{
			this.task = task;
		}

		public void Completed(Action<TParameter> action)
		{
			if (task == null)
			{
				action(value);
				return;
			}

			task.ContinueWith(t => action(t.Result));
		}

		public void UpdateCollection(IObservableCollection<TParameter> collection)
		{
			if (task == null)
			{
				collection.ReplaceOrAdd(value);
				return;
			}

			task.ContinueWith(t => collection.ReplaceOrAdd(t.Result), WindowsThreadingFactory.FromCurrentSynchronizationContext());
		}
	}
}