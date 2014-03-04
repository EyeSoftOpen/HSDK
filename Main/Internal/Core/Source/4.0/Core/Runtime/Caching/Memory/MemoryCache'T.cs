namespace EyeSoft.Runtime.Caching
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Linq;
	using System.Runtime.Caching;

	public class MemoryCache<T> : ICache<T>
	{
		private readonly System.Runtime.Caching.MemoryCache cache;

		public MemoryCache() : this(new System.Runtime.Caching.MemoryCache(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)))
		{
		}

		public MemoryCache(string name) : this(new System.Runtime.Caching.MemoryCache(name))
		{
		}

		public MemoryCache(System.Runtime.Caching.MemoryCache cache)
		{
			this.cache = cache;
			
			DefaultCacheCapabilities = cache.DefaultCacheCapabilities;

			Name = cache.Name;
		}

		public DefaultCacheCapabilities DefaultCacheCapabilities { get; private set; }

		public string Name { get; private set; }

		public long Count
		{
			get { return cache.GetCount(); }
		}

		public T this[string key]
		{
			get { return Get(key); }
			set { Add(key, value); }
		}

		public bool Contains(string key, string regionName = null)
		{
			return cache.Contains(key, regionName);
		}

		public bool Add(string key, T value)
		{
			return Add(new CacheItem(key, value), new CacheItemPolicy());
		}

		public bool Add(string key, T value, DateTimeOffset absoluteExpiration, string regionName = null)
		{
			return cache.Add(key, value, absoluteExpiration, regionName);
		}

		public bool Add(CacheItem item, CacheItemPolicy policy)
		{
			return cache.Add(item, policy);
		}

		public bool Add(string key, T value, CacheItemPolicy policy, string regionName = null)
		{
			return cache.Add(key, key, policy, regionName);
		}

		public T AddOrGetExisting(string key, T value, DateTimeOffset absoluteExpiration, string regionName = null)
		{
			return (T)cache.AddOrGetExisting(key, key, absoluteExpiration, regionName);
		}

		public CacheItem AddOrGetExisting(CacheItem item, CacheItemPolicy policy)
		{
			return cache.AddOrGetExisting(item, policy);
		}

		public T AddOrGetExisting(string key, T value, CacheItemPolicy policy, string regionName = null)
		{
			return (T)cache.AddOrGetExisting(key, value, policy, regionName);
		}

		public T Get(string key, string regionName = null)
		{
			return (T)cache.Get(key, regionName);
		}

		public T Remove(string key, string regionName = null)
		{
			return (T)cache.Remove(key, regionName);
		}

		public IDictionary<string, T> GetValues(string regionName, params string[] keys)
		{
			return GetValues(keys, regionName);
		}

		public IDictionary<string, T> GetValues(IEnumerable<string> keys, string regionName = null)
		{
			return cache.GetValues(keys, regionName).ToDictionary(x => x.Key, v => (T)v.Value);
		}

		public CacheItem GetCacheItem(string key, string regionName = null)
		{
			return cache.GetCacheItem(key, regionName);
		}

		public long GetCount(string regionName = null)
		{
			return cache.GetCount(regionName);
		}

		public bool TryGetValue(string key, out T value)
		{
			if (cache.Contains(key))
			{
				value = (T)cache.Get(key);
				return true;
			}

			value = default(T);
			return false;
		}

		public void Clear()
		{
			cache.Trim(100);
		}

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable)cache).GetEnumerator();
		}

		public void Dispose()
		{
			cache.Dispose();
		}
	}
}