namespace EyeSoft.Data.Nhibernate.Caching.Memory
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.Caching;

	using NHibernate;
	using NHibernate.Cache;

	public class MemoryCache : ICache
	{
		private const string DefaultRegionPrefix = "";
		private const string CacheKeyPrefix = "NHibernate-Cache:";

		private static readonly IInternalLogger log = LoggerProvider.LoggerFor(typeof(MemoryCache));
		private static readonly TimeSpan defaultExpiration = TimeSpan.FromSeconds(300);

		private readonly string region;
		private readonly System.Runtime.Caching.MemoryCache cache;

		// The name of the cache key used to clear the cache. All cached items depend on this key.
		private readonly string rootCacheKey;

		private string regionPrefix;
		private TimeSpan expiration;
		private CacheItemPriority priority;

		private bool rootCacheKeyStored;

		public MemoryCache()
			: this("nhibernate", null)
		{
		}

		public MemoryCache(string region)
			: this(region, null)
		{
		}

		public MemoryCache(string region, IDictionary<string, string> properties)
		{
			this.region =
				string.IsNullOrEmpty(region) ?
				Guid.NewGuid().ToString() :
				region;

			cache = new System.Runtime.Caching.MemoryCache(this.region);
			Configure(properties);

			rootCacheKey = GenerateRootCacheKey();
			StoreRootCacheKey();
		}

		public string Region
		{
			get { return region; }
		}

		public TimeSpan Expiration
		{
			get { return expiration; }
		}

		public CacheItemPriority Priority
		{
			get { return priority; }
		}

		public int Timeout
		{
			get { return Timestamper.OneMs * 60000; } // 60 seconds
		}

		public string RegionName
		{
			get { return region; }
		}

		public object Get(object key)
		{
			if (key == null)
			{
				return null;
			}

			var cacheKey = GetCacheKey(key);

			if (log.IsDebugEnabled)
			{
				log.Debug(string.Format("Fetching object '{0}' from the cache.", cacheKey));
			}

			var obj = cache.Get(cacheKey);
			if (obj == null)
			{
				return null;
			}

			var de = (DictionaryEntry)obj;

			if (key.Equals(de.Key))
			{
				return de.Value;
			}

			return null;
		}

		public void Put(object key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key", "null key not allowed");
			}

			if (value == null)
			{
				throw new ArgumentNullException("value", "null value not allowed");
			}

			var cacheKey = GetCacheKey(key);

			if (cache[cacheKey] != null)
			{
				if (log.IsDebugEnabled)
				{
					log.Debug(string.Format("updating value of key '{0}' to '{1}'.", cacheKey, value));
				}

				// Remove the key to re-add it again below
				cache.Remove(cacheKey);
			}
			else
			{
				if (log.IsDebugEnabled)
				{
					log.Debug(string.Format("adding new data: key={0}&value={1}", cacheKey, value));
				}
			}

			if (!rootCacheKeyStored)
			{
				StoreRootCacheKey();
			}

			var entry = new DictionaryEntry(key, value);
			var cacheItemPolicy = new CacheItemPolicy
				{
					AbsoluteExpiration = DateTime.Now.Add(expiration),
					SlidingExpiration = ObjectCache.NoSlidingExpiration
				};

			cache.Add(cacheKey, entry, cacheItemPolicy);
		}

		public void Remove(object key)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			var cacheKey = GetCacheKey(key);

			if (log.IsDebugEnabled)
			{
				log.Debug("removing item with key: " + cacheKey);
			}

			cache.Remove(cacheKey);
		}

		public void Clear()
		{
			cache.ToList().ForEach(item => cache.Remove(item.Key));

			RemoveRootCacheKey();
			StoreRootCacheKey();
		}

		public void Destroy()
		{
			Clear();
		}

		public void Lock(object key)
		{
			// Do nothing
		}

		public void Unlock(object key)
		{
			// Do nothing
		}

		public long NextTimestamp()
		{
			return Timestamper.Next();
		}

		private static string GetRegionPrefix(IDictionary<string, string> props)
		{
			string result;
			if (props.TryGetValue("regionPrefix", out result))
			{
				log.DebugFormat("new regionPrefix :{0}", result);
			}
			else
			{
				result = DefaultRegionPrefix;
				log.Debug("no regionPrefix value given, using defaults");
			}

			return result;
		}

		private static TimeSpan GetExpiration(IDictionary<string, string> props)
		{
			var result = defaultExpiration;
			string expirationString;

			if (!props.TryGetValue("expiration", out expirationString))
			{
				props.TryGetValue(NHibernate.Cfg.Environment.CacheDefaultExpiration, out expirationString);
			}

			if (expirationString != null)
			{
				try
				{
					int seconds = Convert.ToInt32(expirationString);
					result = TimeSpan.FromSeconds(seconds);
					log.Debug("new expiration value: " + seconds);
				}
				catch (Exception ex)
				{
					log.Error("error parsing expiration value");
					throw new ArgumentException("could not parse 'expiration' as a number of seconds", ex);
				}
			}
			else
			{
				if (log.IsDebugEnabled)
				{
					log.Debug("no expiration value given, using defaults");
				}
			}

			return result;
		}

		private static CacheItemPriority GetPriority(IDictionary<string, string> props)
		{
			var result = CacheItemPriority.Default;

			string priorityString;

			if (props.TryGetValue("priority", out priorityString))
			{
				result = ConvertCacheItemPriorityFromXmlString(priorityString);
				if (log.IsDebugEnabled)
				{
					log.Debug("new priority: " + result);
				}
			}

			return result;
		}

		private static CacheItemPriority ConvertCacheItemPriorityFromXmlString(string priorityString)
		{
			if (string.IsNullOrEmpty(priorityString))
			{
				return CacheItemPriority.Default;
			}

			var ps = priorityString.Trim().ToLowerInvariant();

			if (ps.Length == 1 && char.IsDigit(priorityString, 0))
			{
				// the priority is specified as a number
				var priorityAsInt = int.Parse(ps);

				if (priorityAsInt >= 1 && priorityAsInt <= 6)
				{
					return (CacheItemPriority)priorityAsInt;
				}
			}
			else
			{
				switch (ps)
				{
					case "abovenormal":
						return CacheItemPriority.Default;
					case "belownormal":
						return CacheItemPriority.Default;
					case "default":
						return CacheItemPriority.Default;
					case "high":
						return CacheItemPriority.Default;
					case "low":
						return CacheItemPriority.Default;
					case "normal":
						return CacheItemPriority.Default;
					case "notremovable":
						return CacheItemPriority.NotRemovable;
				}
			}

			log.Error("priority value out of range: " + priorityString);
			throw new IndexOutOfRangeException("Priority must be a valid System.Web.Caching.CacheItemPriority; was: " + priorityString);
		}

		private void Configure(IDictionary<string, string> props)
		{
			if (props == null)
			{
				if (log.IsWarnEnabled)
				{
					log.Warn("configuring cache with default values");
				}

				expiration = defaultExpiration;
				priority = CacheItemPriority.Default;
				regionPrefix = DefaultRegionPrefix;
			}
			else
			{
				priority = GetPriority(props);
				expiration = GetExpiration(props);
				regionPrefix = GetRegionPrefix(props);
			}
		}

		private string GetCacheKey(object key)
		{
			return
				string.Concat(CacheKeyPrefix, regionPrefix, region, ":", key.ToString(), "@", key.GetHashCode());
		}

		/// <summary>
		/// Generate a unique root key for all cache items to be dependant upon
		/// </summary>
		private string GenerateRootCacheKey()
		{
			return GetCacheKey(Guid.NewGuid());
		}

		private void RootCacheItemRemoved(string key, object value, CacheEntryRemovedReason reason)
		{
			rootCacheKeyStored = false;
		}

		private void StoreRootCacheKey()
		{
			rootCacheKeyStored = true;
			cache.Add(rootCacheKey, rootCacheKey, new CacheItemPolicy());
		}

		private void RemoveRootCacheKey()
		{
			cache.Remove(rootCacheKey);
		}
	}
}