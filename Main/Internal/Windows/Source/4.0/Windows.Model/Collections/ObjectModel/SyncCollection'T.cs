namespace EyeSoft.Windows.Model.Collections.ObjectModel
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Runtime.InteropServices;
	using System.Threading;

	[Serializable]
	[ComVisible(false)]
	[DebuggerDisplay("Count = {Count}")]
	public class SyncedCollection<T> : IList<T>, IList
	{
		private readonly List<T> items;

		[NonSerialized]
		private object syncRoot;

		public SyncedCollection()
		{
			items = new List<T>();
		}

		public SyncedCollection(IEnumerable<T> list) : this()
		{
			items.AddRange(list);
		}

		public int Count
		{
			get
			{
				return items.Count;
			}
		}

		bool ICollection<T>.IsReadOnly
		{
			get
			{
				return ((IList<T>)items).IsReadOnly;
			}
		}

		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		object ICollection.SyncRoot
		{
			get
			{
				if (syncRoot == null)
				{
					var collection = items as ICollection;

					if (collection != null)
					{
						syncRoot = collection.SyncRoot;
					}
					else
					{
						Interlocked.CompareExchange<object>(ref syncRoot, new object(), null);
					}
				}
				return syncRoot;
			}
		}

		bool IList.IsFixedSize
		{
			get
			{
				var list = items as IList;
				if (list != null)
				{
					return list.IsFixedSize;
				}
				return list.IsReadOnly;
			}
		}

		bool IList.IsReadOnly
		{
			get
			{
				return ((IList<T>)items).IsReadOnly;
			}
		}

		protected IList<T> Items
		{
			get
			{
				return items;
			}
		}

		public T this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				SetItem(index, value);
			}
		}

		object IList.this[int index]
		{
			get
			{
				return items[index];
			}
			set
			{
				this[index] = (T)value;
			}
		}

		public void Add(T item)
		{
			var count = items.Count;
			InsertItem(count, item);
		}

		public virtual void AddRange(IEnumerable<T> collection)
		{
			items.AddRange(collection);
		}

		public void Clear()
		{
			ClearItems();
		}

		public bool Contains(T item)
		{
			return items.Contains(item);
		}

		public void CopyTo(T[] array, int index)
		{
			items.CopyTo(array, index);
		}

		public IEnumerator<T> GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public int IndexOf(T item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, T item)
		{
			InsertItem(index, item);
		}

		public bool Remove(T item)
		{
			var index = items.IndexOf(item);

			if (index < 0)
			{
				return false;
			}

			RemoveItem(index);
			return true;
		}

		public void RemoveAt(int index)
		{
			RemoveItem(index);
		}

		void ICollection.CopyTo(Array array, int index)
		{
			var localArray = array as T[];

			if (localArray != null)
			{
				items.CopyTo(localArray, index);
			}
			else
			{
				var objArray = array as object[];
				var count = items.Count;
				for (var i = 0; i < count; i++)
				{
					objArray[index++] = items[i];
				}
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		int IList.Add(object value)
		{
			Add((T)value);
			return Count - 1;
		}

		bool IList.Contains(object value)
		{
			return IsCompatibleObject(value) && Contains((T)value);
		}

		int IList.IndexOf(object value)
		{
			if (IsCompatibleObject(value))
			{
				return IndexOf((T)value);
			}

			return -1;
		}

		void IList.Insert(int index, object value)
		{
			Insert(index, (T)value);
		}

		void IList.Remove(object value)
		{
			if (IsCompatibleObject(value))
			{
				Remove((T)value);
			}
		}

		protected virtual void ClearItems()
		{
			items.Clear();
		}

		protected virtual void RemoveItem(int index)
		{
			items.RemoveAt(index);
		}

		protected virtual void SetItem(int index, T item)
		{
			items[index] = item;
		}

		protected virtual void InsertItem(int index, T item)
		{
			items.Insert(index, item);
		}

		private static bool IsCompatibleObject(object value)
		{
			return (value is T) || ((value == null) && Equals(default(T), null));
		}
	}
}