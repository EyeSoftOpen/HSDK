namespace EyeSoft.Collections.Concurrent
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;

	public class TypeOnceCollection<T> : ICollection<T>
	{
		private readonly Collections.Concurrent.SafeConcurrentDictionary<Type, T> safeConcurrentDictionary =
			new Collections.Concurrent.SafeConcurrentDictionary<Type, T>();

		public int Count
		{
			get { return safeConcurrentDictionary.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		public IEnumerator<T> GetEnumerator()
		{
			return safeConcurrentDictionary.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public void Add(T item)
		{
			var itemType = item.GetType();

			safeConcurrentDictionary.Add(itemType, item);
		}

		public void Clear()
		{
			safeConcurrentDictionary.Clear();
		}

		public bool Contains(T item)
		{
			var containsType = safeConcurrentDictionary.Keys.Contains(item.GetType());

			var containsItem = safeConcurrentDictionary.Values.Contains(item);

			return containsType || containsItem;
		}

		public void CopyTo(T[] array, int arrayIndex)
		{
			safeConcurrentDictionary.Values.CopyTo(array, arrayIndex);
			safeConcurrentDictionary.Keys.CopyTo(array.Select(x => x.GetType()).ToArray(), arrayIndex);
		}

		public bool Remove(T item)
		{
			T value;
			return safeConcurrentDictionary.TryRemove(item.GetType(), out value);
		}
	}
}