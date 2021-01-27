namespace EyeSoft.Core.Collections.Concurrent
{
    using System.Collections.Concurrent;
    using System.Collections.Generic;

    public class SafeConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, TValue>, IDictionary<TKey, TValue>
	{
		private readonly object lockInstance = new object();

		public new bool ContainsKey(TKey key)
		{
			lock (lockInstance)
			{
				return base.ContainsKey(key);
			}
		}

		public void Add(TKey key, TValue value)
		{
			lock (lockInstance)
			{
				TryAdd(key, value);
			}
		}

		bool IDictionary<TKey, TValue>.ContainsKey(TKey key)
		{
			return ContainsKey(key);
		}

		void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
		{
			Add(key, value);
		}
	}
}