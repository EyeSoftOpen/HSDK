// Copyright (c) Microsoft Corporation.  All rights reserved.

namespace EyeSoft.Data.EntityFramework.Caching.Test
{
	using System;
	using System.Collections.Generic;
	using System.Text;
	using System.Threading;


	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class InMemoryCacheTests
	{
		[TestMethod]
		public void InMemoryCacheInitialStatisticsAreAllZero()
		{
			var cacheWithStatistics = new EntityFrameworkCache();
			Assert.AreEqual(0, cacheWithStatistics.Hits);
			Assert.AreEqual(0, cacheWithStatistics.ItemAdds);
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);
			Assert.AreEqual(0, cacheWithStatistics.Misses);
		}

		[TestMethod]
		public void InMemoryCacheCacheMissTests()
		{
			var cacheWithStatistics = new EntityFrameworkCache();
			object value;

			Assert.IsFalse(cacheWithStatistics.GetItem("NoSuchItem", out value));
			Assert.IsNull(value);
			Assert.AreEqual(0, cacheWithStatistics.Hits);
			Assert.AreEqual(1, cacheWithStatistics.Misses);
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);
			Assert.AreEqual(0, cacheWithStatistics.ItemAdds);
		}

		[TestMethod]
		public void InMemoryCacheCacheHitTests()
		{
			var cacheWithStatistics = new EntityFrameworkCache();
			object value;

			cacheWithStatistics.PutItem("Item1", "someValue", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			Assert.IsTrue(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(value, "someValue");
			Assert.AreEqual(1, cacheWithStatistics.Hits);
			Assert.AreEqual(0, cacheWithStatistics.Misses);
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);
			Assert.AreEqual(1, cacheWithStatistics.ItemAdds);
		}

		[TestMethod]
		public void InMemoryCacheCacheReplaceTests()
		{
			var cacheWithStatistics = new EntityFrameworkCache();

			object value;

			cacheWithStatistics.PutItem("Item1", "someValue", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			cacheWithStatistics.PutItem("Item1", "someOtherValue", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			Assert.IsTrue(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(value, "someOtherValue");
			Assert.AreEqual(1, cacheWithStatistics.Hits);
			Assert.AreEqual(0, cacheWithStatistics.Misses);
			Assert.AreEqual(1, cacheWithStatistics.ItemInvalidations);
			Assert.AreEqual(2, cacheWithStatistics.ItemAdds);
		}

		[TestMethod]
		public void InMemoryCacheCacheExpirationTest1()
		{
			var cacheWithStatistics = new EntityFrameworkCache();

			object value;

			// current time is 10:00
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 0, 0);

			// set expiration time to 11:00
			cacheWithStatistics.PutItem("Item1", "someValue", new string[0], TimeSpan.Zero, new DateTime(2009, 1, 1, 11, 0, 0));

			// make sure the item is still there
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 59, 59);
			Assert.IsTrue(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(value, "someValue");
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);

			// make sure the item gets evicted at 11:00
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 11, 00, 00);
			Assert.IsFalse(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(1, cacheWithStatistics.ItemInvalidations);
		}

		[TestMethod]
		public void InMemoryCacheCacheExpirationTest2()
		{
			var cacheWithStatistics = new EntityFrameworkCache();

			object value;

			// current time is 10:00
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 0, 0);

			// set expiration time to 1 hour from the last access
			cacheWithStatistics.PutItem("Item1", "someValue", new string[0], TimeSpan.FromHours(1), DateTime.MaxValue);

			// make sure the item is still there
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 59, 59);
			Assert.IsTrue(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(value, "someValue");
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);

			// make sure the item does not get evicted at 11:00 because we have touched it a second ago
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 11, 00, 00);
			Assert.IsTrue(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(value, "someValue");
			Assert.AreEqual(0, cacheWithStatistics.ItemInvalidations);

			Assert.IsNotNull(cacheWithStatistics.LruChainHead);

			// make sure the item does not get evicted at 12:00 because we have touched it an hour ago
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 12, 00, 00);
			Assert.IsFalse(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual(1, cacheWithStatistics.ItemInvalidations);

			Assert.IsNull(cacheWithStatistics.LruChainHead);
			Assert.IsNull(cacheWithStatistics.LruChainTail);
		}

		[TestMethod]
		public void InMemoryCacheCacheExpirationTest3()
		{
			var cacheWithStatistics = new EntityFrameworkCache();

			object value;

			// current time is 10:00
			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 0, 0);
			cacheWithStatistics.PutItem("Item1", "someValue", new string[0], TimeSpan.Zero, new DateTime(2009, 1, 1, 10, 0, 0));
			cacheWithStatistics.PutItem("Item2", "someValue", new string[0], TimeSpan.Zero, new DateTime(2009, 1, 1, 10, 1, 0));
			cacheWithStatistics.PutItem("Item3", "someValue", new string[0], TimeSpan.Zero, new DateTime(2009, 1, 1, 10, 2, 0));
			cacheWithStatistics.PutItem("Item4", "someValue", new string[0], TimeSpan.Zero, new DateTime(2009, 1, 1, 10, 3, 0));

			cacheWithStatistics.GetCurrentDate = () => new DateTime(2009, 1, 1, 10, 2, 0);

			// no invalidation happens until we try to get an item
			Assert.AreEqual("Item4|Item3|Item2|Item1", GetItemKeysInLruOrder(cacheWithStatistics));

			Assert.IsFalse(cacheWithStatistics.GetItem("Item1", out value));
			Assert.AreEqual("Item4", GetItemKeysInLruOrder(cacheWithStatistics));
		}

		[TestMethod]
		public void LruTest1()
		{
			var imc = new EntityFrameworkCache();
			Assert.AreEqual(int.MaxValue, imc.MaxItems);

			imc.PutItem("A", "A1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new string[0], TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));

			object value;
			imc.GetItem("D", out value);
			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));
			imc.GetItem("C", out value);
			Assert.AreEqual("C|D|B|A", GetItemKeysInLruOrder(imc));
			imc.GetItem("D", out value);
			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));
			imc.GetItem("A", out value);
			Assert.AreEqual("A|D|C|B", GetItemKeysInLruOrder(imc));
			imc.GetItem("B", out value);
			Assert.AreEqual("B|A|D|C", GetItemKeysInLruOrder(imc));
			imc.InvalidateItem("B");
			Assert.AreEqual("A|D|C", GetItemKeysInLruOrder(imc));
			imc.InvalidateItem("C");
			Assert.AreEqual("A|D", GetItemKeysInLruOrder(imc));
			imc.InvalidateItem("A");
			Assert.AreEqual("D", GetItemKeysInLruOrder(imc));
			imc.InvalidateItem("D");
			Assert.AreEqual(string.Empty, GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void LruWithLimitTest2()
		{
			var imc = new EntityFrameworkCache(3);

			Assert.AreEqual(3, imc.MaxItems);

			imc.PutItem("A", "A1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new string[0], TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B", GetItemKeysInLruOrder(imc));
			imc.PutItem("E", "E1", new string[0], TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("E|D|C", GetItemKeysInLruOrder(imc));
			imc.PutItem("F", "F1", new string[0], TimeSpan.Zero, DateTime.MaxValue);
			Assert.AreEqual("F|E|D", GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void DependenciesTest()
		{
			var imc = new EntityFrameworkCache();

			imc.PutItem("A", "A1", new[] { "set1" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new[] { "set1", "set2" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new[] { "set2", "set3" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new[] { "set3", "set1" }, TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));

			imc.InvalidateSets(new[] { "set2" });
			Assert.AreEqual("D|A", GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void DependenciesTest2()
		{
			var imc = new EntityFrameworkCache();

			imc.PutItem("A", "A1", new[] { "set1" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new[] { "set1", "set2" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new[] { "set2", "set3" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new[] { "set3", "set1" }, TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));

			imc.InvalidateSets(new[] { "set1" });
			Assert.AreEqual("C", GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void DependenciesTest3()
		{
			var imc = new EntityFrameworkCache();

			imc.PutItem("A", "A1", new[] { "set1" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new[] { "set1", "set2" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new[] { "set2", "set3" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new[] { "set3", "set1" }, TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));

			imc.InvalidateSets(new[] { "set77" });
			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void DependenciesTest4()
		{
			var imc = new EntityFrameworkCache();

			imc.PutItem("A", "A1", new[] { "set1" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("B", "B1", new[] { "set1", "set2" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("C", "C1", new[] { "set2", "set3" }, TimeSpan.Zero, DateTime.MaxValue);
			imc.PutItem("D", "D1", new[] { "set3", "set1" }, TimeSpan.Zero, DateTime.MaxValue);

			Assert.AreEqual("D|C|B|A", GetItemKeysInLruOrder(imc));
			imc.PutItem("B", "C1", new[] { "set3" }, TimeSpan.Zero, DateTime.MaxValue);
			Assert.AreEqual("B|D|C|A", GetItemKeysInLruOrder(imc));

			imc.InvalidateSets(new[] { "set3" });
			Assert.AreEqual("A", GetItemKeysInLruOrder(imc));
		}

		[TestMethod]
		public void MicroStressTest()
		{
			// Run a bunch of concurrent reads and writes, make sure we get no exceptions
			var imc = new EntityFrameworkCache(100);
			const int NumberOfRequestBatches = 50; // will be multiplied by 5 (3 readers + 1 writer + 1 invalidator)
			const int NumberOfIterationsPerThread = 100;

			var startEvent = new ManualResetEvent(false);

			Action writer = () =>
			{
				startEvent.WaitOne();
				var random = new Random();

				for (var i = 0; i < NumberOfIterationsPerThread; ++i)
				{
					var randomKey = Guid.NewGuid().ToString("N").Substring(0, 4);
					var randomValue = randomKey + "_V";
					var dependentSets = new List<string>();
					var numberOfDependencies = random.Next(5);

					for (var j = 0; j < numberOfDependencies; ++j)
					{
						var randomSetName = new string((char)('A' + random.Next(26)), 1);
						dependentSets.Add(randomSetName);
					}

					imc.PutItem(randomKey, randomValue, dependentSets, TimeSpan.Zero, DateTime.MaxValue);
				}
			};

			Action invalidator = () =>
			{
				startEvent.WaitOne();
				var random = new Random();

				for (var i = 0; i < NumberOfIterationsPerThread; ++i)
				{
					var dependentSets = new List<string>();
					var numberOfDependencies = random.Next(5);
					for (var j = 0; j < numberOfDependencies; ++j)
					{
						var randomSetName = new string((char)('A' + random.Next(26)), 1);
						dependentSets.Add(randomSetName);
					}

					imc.InvalidateSets(dependentSets);
				}
			};

			Action reader = () =>
			{
				startEvent.WaitOne();

				for (var i = 0; i < NumberOfIterationsPerThread; ++i)
				{
					var randomKey = Guid.NewGuid().ToString("N").Substring(0, 4);
					object value;

					if (imc.GetItem(randomKey, out value))
					{
						Assert.AreEqual(randomKey + "_V", value);
					}
				}
			};

			var threads = new List<Thread>();

			for (var i = 0; i < NumberOfRequestBatches; ++i)
			{
				threads.Add(new Thread(() => writer()));
				threads.Add(new Thread(() => invalidator()));
				threads.Add(new Thread(() => reader()));
				threads.Add(new Thread(() => reader()));
				threads.Add(new Thread(() => reader()));
			}

			foreach (var t in threads)
			{
				t.Start();
			}

			startEvent.Set();

			foreach (var t in threads)
			{
				t.Join();
			}
		}

		private static string GetItemKeysInLruOrder(EntityFrameworkCache imc)
		{
			var sb = new StringBuilder();
			var separator = string.Empty;
			var visisted = new HashSet<string>();

			CacheEntry lastEntry = null;

			for (var ce = imc.LruChainHead; ce != null; ce = ce.NextEntry)
			{
				if (visisted.Contains(ce.Key))
				{
					throw new InvalidOperationException("Cycle in the LRU chain on: " + ce);
				}

				if (!Equals(ce.PreviousEntry, lastEntry))
				{
					var message = "Invalid previous pointer on LRU chain: " + ce + " Is: " + ce.PreviousEntry + " expected: " + lastEntry;
					throw new InvalidOperationException(message);
				}

				if (lastEntry != null && ce.LastAccessTime.Subtract(lastEntry.LastAccessTime).TotalMilliseconds > 500)
				{
					var message =
						"Invalid LastAccess time. Current: " + ce.LastAccessTime.Ticks + " previous: " + lastEntry.LastAccessTime.Ticks;
					throw new InvalidOperationException(message);
				}

				sb.Append(separator);
				sb.Append(ce.Key);
				separator = "|";
				visisted.Add(ce.Key);
				lastEntry = ce;
			}

			Assert.AreSame(lastEntry, imc.LruChainTail);

			return sb.ToString();
		}
	}
}