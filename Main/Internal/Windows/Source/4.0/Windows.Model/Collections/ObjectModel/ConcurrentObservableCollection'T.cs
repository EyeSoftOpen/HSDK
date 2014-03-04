namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System;
	using System.Collections.Generic;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Windows.Threading;

	[Serializable]
	public class ConcurrentObservableCollection<T> : SyncedCollection<T>, IObservableCollection<T>
	{
		protected readonly object syncLock = new object();

		private readonly SimpleMonitor monitor;

		public ConcurrentObservableCollection()
		{
			// Valid from FW 4.5 or newer
			//// BindingOperations.EnableCollectionSynchronization(this, syncLock);

			monitor = new SimpleMonitor();
		}

		public ConcurrentObservableCollection(IEnumerable<T> collection) : this()
		{
			monitor = new SimpleMonitor();

			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}

			CopyFrom(collection);
		}

		public ConcurrentObservableCollection(ICollection<T> list)
			: base((list != null) ? new List<T>(list.Count) : null)
		{
			monitor = new SimpleMonitor();
			CopyFrom(list);
		}

		[field: NonSerialized]
		public event NotifyCollectionChangedEventHandler CollectionChanged;

		[field: NonSerialized]
		protected event PropertyChangedEventHandler PropertyChanged;

		event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
		{
			add
			{
				PropertyChanged += value;
			}
			remove
			{
				PropertyChanged -= value;
			}
		}

		public void Move(int oldIndex, int newIndex)
		{
			MoveItem(oldIndex, newIndex);
		}

		public override void AddRange(IEnumerable<T> collection)
		{
			CheckReentrancy();

			base.AddRange(collection);

			OnCollectionReset();
		}

		public void ReplaceOrAdd(T item)
		{
			lock (syncLock)
			{
				var index = IndexOf(item);

				if (index >= 0)
				{
					SetItem(index, item);
					return;
				}

				Add(item);
			}
		}

		public void Remove(IEnumerable<T> collection)
		{
			foreach (var item in collection)
			{
				Remove(item);
			}
		}

		protected IDisposable BlockReentrancy()
		{
			monitor.Enter();
			return monitor;
		}

		protected void CheckReentrancy()
		{
			if ((monitor.Busy && (CollectionChanged != null)) && (CollectionChanged.GetInvocationList().Length > 1))
			{
				throw new InvalidOperationException("ObservableCollectionReentrancyNotAllowed");
			}
		}

		protected override void ClearItems()
		{
			CheckReentrancy();
			base.ClearItems();
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionReset();
		}

		protected override void InsertItem(int index, T item)
		{
			CheckReentrancy();
			base.InsertItem(index, item);
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
		}

		protected virtual void MoveItem(int oldIndex, int newIndex)
		{
			CheckReentrancy();
			var item = base[oldIndex];
			base.RemoveItem(oldIndex);
			base.InsertItem(newIndex, item);
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex);
		}

		protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
		{
			Changed(e);
		}

		protected virtual void Changed(NotifyCollectionChangedEventArgs e)
		{
			// Recommended is to avoid reentry
			// in collection changed event while collection
			// is getting changed on other thread.
			using (BlockReentrancy())
			{
				var eventHandler = CollectionChanged;

				if (eventHandler == null)
				{
					return;
				}

				var delegates = eventHandler.GetInvocationList();

				foreach (NotifyCollectionChangedEventHandler handler in delegates)
				{
					// If the subscriber is a DispatcherObject and different thread
					var dispatcherObject = handler.Target as DispatcherObject;

					if (dispatcherObject != null && !dispatcherObject.CheckAccess())
					{
						// Invoke handler in the target dispatcher's thread
						// asynchronously for better responsiveness.
						dispatcherObject.Dispatcher.BeginInvoke(DispatcherPriority.DataBind, handler, this, e);
					}
					else
					{
						// Execute handler as is
						handler(this, e);
					}
				}
			}
		}

		protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, e);
			}
		}

		protected override void RemoveItem(int index)
		{
			CheckReentrancy();
			T item = base[index];
			base.RemoveItem(index);
			OnPropertyChanged("Count");
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
		}

		protected override void SetItem(int index, T item)
		{
			CheckReentrancy();
			T oldItem = base[index];
			base.SetItem(index, item);
			OnPropertyChanged("Item[]");
			OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
		}

		private void CopyFrom(IEnumerable<T> collection)
		{
			var items = Items;

			if ((collection == null) || (items == null))
			{
				return;
			}

			using (var enumerator = collection.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					items.Add(enumerator.Current);
				}
			}
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index, int oldIndex)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index, oldIndex));
		}

		private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
		}

		private void OnCollectionReset()
		{
			OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		}

		private void OnPropertyChanged(string propertyName)
		{
			OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
		}

		[Serializable]
		private class SimpleMonitor : IDisposable
		{
			private int busyCount;

			public bool Busy
			{
				get
				{
					return busyCount > 0;
				}
			}

			public void Dispose()
			{
				busyCount--;
			}

			public void Enter()
			{
				busyCount++;
			}
		}
	}
}