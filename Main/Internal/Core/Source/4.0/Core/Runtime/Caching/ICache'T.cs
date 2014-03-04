namespace EyeSoft.Runtime.Caching
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Runtime.Caching;

	public interface ICache<T> : IEnumerable, IDisposable
	{
		DefaultCacheCapabilities DefaultCacheCapabilities { get; }
	
		string Name { get; }

		long Count { get; }

		T this[string key] { get; set; }

		bool Contains(string key, string regionName = null);

		bool Add(CacheItem item, CacheItemPolicy policy);

		bool Add(string key, T value);

		bool Add(string key, T value, CacheItemPolicy policy, string regionName = null);

		bool Add(string key, T value, DateTimeOffset absoluteExpiration, string regionName = null);

		T AddOrGetExisting(string key, T value, CacheItemPolicy policy, string regionName = null);

		T AddOrGetExisting(string key, T value, DateTimeOffset absoluteExpiration, string regionName = null);

		CacheItem AddOrGetExisting(CacheItem item, CacheItemPolicy policy);

		T Get(string key, string regionName = null);

		T Remove(string key, string regionName = null);

		IDictionary<string, T> GetValues(IEnumerable<string> keys, string regionName = null);

		IDictionary<string, T> GetValues(string regionName, params string[] keys);

		CacheItem GetCacheItem(string key, string regionName = null);
		
		long GetCount(string regionName = null);

		bool TryGetValue(string key, out T value);

		void Clear();
	}
}