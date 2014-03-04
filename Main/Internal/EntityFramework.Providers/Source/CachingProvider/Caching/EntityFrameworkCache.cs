namespace EyeSoft.Data.EntityFramework.Caching
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics;
	using System.Linq;
	using System.Threading;

	using EyeSoft.Runtime.Caching;

	public sealed class EntityFrameworkCache : ICacheWithStatistics, IDisposable
	{
		private readonly ICache cache;

		private readonly object lruLock = new object();

		private readonly ReaderWriterLockSlim entriesLock = new ReaderWriterLockSlim();

		private int cacheHits;
		private int misses;
		private int adds;
		private int itemInvalidations;

		public EntityFrameworkCache()
			: this(new MemoryCache())
		{
		}

		public EntityFrameworkCache(int maxItems)
			: this(new MemoryCache(), maxItems)
		{
		}

		public EntityFrameworkCache(ICache cache, int maxItems = int.MaxValue)
		{
			this.cache = cache;
			MaxItems = maxItems;
			GetCurrentDate = () => DateTime.Now;
		}

		public int Hits
		{
			get { return cacheHits; }
		}

		public int Misses
		{
			get { return misses; }
		}

		public int ItemAdds
		{
			get { return adds; }
		}

		public int ItemInvalidations
		{
			get { return itemInvalidations; }
		}

		public int MaxItems { get; private set; }

		public long Count
		{
			get
			{
				entriesLock.EnterReadLock();
				var count = cache.Count;
				entriesLock.ExitReadLock();
				return count;
			}
		}

		internal Func<DateTime> GetCurrentDate { get; set; }

		internal CacheEntry LruChainHead { get; private set; }

		internal CacheEntry LruChainTail { get; private set; }

		public bool GetItem(string key, out object value)
		{
			CacheEntry entry;
			var currentDate = GetCurrentDate();

			entriesLock.EnterReadLock();

			var success = cache.TryGetValue(key, out entry);
			entriesLock.ExitReadLock();

			if (success)
			{
				if (currentDate >= entry.ExpirationTime)
				{
					success = false;
					InvalidateExpiredEntries();
				}
			}

			if (!success)
			{
				Interlocked.Increment(ref misses);
				value = null;
				return false;
			}

			Interlocked.Increment(ref cacheHits);
			MoveToTopOfLruChain(entry);
			entry.LastAccessTime = GetCurrentDate();
			if (entry.SlidingExpiration > TimeSpan.Zero)
			{
				entry.ExpirationTime = GetCurrentDate().Add(entry.SlidingExpiration);
			}

			value = entry.Value;
			return true;
		}

		public void PutItem(
			string key,
			object value,
			IEnumerable<string> dependentEntitySets,
			TimeSpan slidingExpiration,
			DateTime absoluteExpiration)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}

			if (dependentEntitySets == null)
			{
				throw new ArgumentNullException("dependentEntitySets");
			}

			var entitySets = dependentEntitySets as string[] ?? dependentEntitySets.ToArray();

			var newEntry = new CacheEntry
				{
					Key = key,
					KeyHashCode = key.GetHashCode(),
					Value = value,
					DependentEntitySets = entitySets,
					SlidingExpiration = slidingExpiration,
					ExpirationTime = absoluteExpiration,
				};

			newEntry.ExpirationTime =
				slidingExpiration > TimeSpan.Zero ?
					GetCurrentDate().Add(slidingExpiration) :
					absoluteExpiration;

			entriesLock.EnterWriteLock();

			CacheEntry oldEntry;

			if (cache.TryGetValue(key, out oldEntry))
			{
				InvalidateSingleEntry(oldEntry);
			}

			// too many items in the cache - invalidate the last one in LRU chain
			if (cache.Count >= MaxItems)
			{
				InvalidateSingleEntry(LruChainTail);
			}

			cache.Add(key, newEntry);

			foreach (var entitySet in entitySets)
			{
				HashSet<CacheEntry> queriesDependentOnSet;

				if (!cache.TryGetValue(entitySet, out queriesDependentOnSet))
				{
					queriesDependentOnSet = new HashSet<CacheEntry>();
					cache.Add(entitySet, queriesDependentOnSet);
				}

				queriesDependentOnSet.Add(newEntry);
			}

			Interlocked.Increment(ref adds);
			MoveToTopOfLruChain(newEntry);
			newEntry.LastAccessTime = GetCurrentDate();
			entriesLock.ExitWriteLock();
		}

		public void InvalidateSets(IEnumerable<string> entitySets)
		{
			if (entitySets == null)
			{
				throw new ArgumentNullException("entitySets");
			}

			entriesLock.EnterWriteLock();
			foreach (var entitySet in entitySets)
			{
				HashSet<CacheEntry> dependentEntries;

				if (!cache.TryGetValue(entitySet, out dependentEntries))
				{
					continue;
				}

				foreach (var entry in dependentEntries.ToArray())
				{
					InvalidateSingleEntry(entry);
				}
			}

			entriesLock.ExitWriteLock();
		}

		public void InvalidateItem(string key)
		{
			entriesLock.EnterWriteLock();
			CacheEntry entry;
			if (cache.TryGetValue(key, out entry))
			{
				InvalidateSingleEntry(entry);
			}

			entriesLock.ExitWriteLock();
		}

		public void Dispose()
		{
			cache.Dispose();
			entriesLock.Dispose();
		}

		private void InvalidateSingleEntry(CacheEntry entry)
		{
			RemoveFromLruChain(entry);

			Interlocked.Increment(ref itemInvalidations);
			Debug.Assert(entriesLock.IsWriteLockHeld, "Must be holding write lock");
			cache.Remove(entry.Key);

			foreach (var set in entry.DependentEntitySets)
			{
				cache.Get<HashSet<CacheEntry>>(set).Remove(entry);
			}
		}

		private void MoveToTopOfLruChain(CacheEntry entry)
		{
			lock (lruLock)
			{
				if (Equals(entry, LruChainHead))
				{
					return;
				}

				if (Equals(entry, LruChainTail))
				{
					LruChainTail = LruChainTail.PreviousEntry;
				}

				if (entry.PreviousEntry != null)
				{
					entry.PreviousEntry.NextEntry = entry.NextEntry;
				}

				if (entry.NextEntry != null)
				{
					entry.NextEntry.PreviousEntry = entry.PreviousEntry;
				}

				if (LruChainHead != null)
				{
					LruChainHead.PreviousEntry = entry;
				}

				entry.NextEntry = LruChainHead;
				entry.PreviousEntry = null;
				LruChainHead = entry;

				if (LruChainTail == null)
				{
					LruChainTail = entry;
				}
			}
		}

		private void RemoveFromLruChain(CacheEntry entry)
		{
			lock (lruLock)
			{
				if (Equals(entry, LruChainHead))
				{
					LruChainHead = LruChainHead.NextEntry;
				}

				if (entry.PreviousEntry != null)
				{
					entry.PreviousEntry.NextEntry = entry.NextEntry;
				}
				else
				{
					LruChainHead = entry.NextEntry;
				}

				if (entry.NextEntry != null)
				{
					entry.NextEntry.PreviousEntry = entry.PreviousEntry;
				}
				else
				{
					LruChainTail = entry.PreviousEntry;
				}
			}
		}

		private void InvalidateExpiredEntries()
		{
			entriesLock.EnterWriteLock();

			var now = GetCurrentDate();
			CacheEntry nextEntry;

			for (var entryToExpire = LruChainHead; entryToExpire != null; entryToExpire = nextEntry)
			{
				// Remember this reference as the invalication function will destroy it
				nextEntry = entryToExpire.NextEntry;
				if (now >= entryToExpire.ExpirationTime)
				{
					InvalidateSingleEntry(entryToExpire);
				}
			}

			entriesLock.ExitWriteLock();
		}
	}
}