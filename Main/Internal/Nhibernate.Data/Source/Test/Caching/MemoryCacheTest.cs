namespace EyeSoft.Data.Nhibernate.Test.Caching
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Threading;

	using EyeSoft.Data.Nhibernate.Caching.Memory;

	using log4net.Config;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using NHibernate.Cache;

	using SharpTestsEx;

	[TestClass]
	public class MemoryCacheTest
	{
		private const string RegionName = "test";
		private const int ExpirySeconds = 1;

		private const string Key = "key";
		private const string Value = "value";

		private MemoryCacheProvider provider;
		private Dictionary<string, string> props;

		[TestInitialize]
		public void FixtureSetup()
		{
			XmlConfigurator.Configure();
			props =
				new Dictionary<string, string>
					{
						{ "expiration", 120.ToString(CultureInfo.InvariantCulture) },
						{ "priority", 4.ToString(CultureInfo.InvariantCulture) }
					};
			provider = new MemoryCacheProvider();
		}

		[TestMethod]
		public void TestPut()
		{
			var cache = provider.BuildCache(RegionName, props);

			cache.Should("No cache returned").Not.Be.Null();

			cache.Get(Key).Should("Cache returned an item we didn't add.").Be.Null();

			cache.Put(Key, Value);
			var item = cache.Get(Key);
			Assert.IsNotNull(item);
			Assert.AreEqual(Value, item, "didn't return the item we added");
		}

		[TestMethod]
		public void TestRemove()
		{
			var cache = provider.BuildCache(RegionName, props);
			Assert.IsNotNull(cache, "no cache returned");

			// add the item
			cache.Put(Key, Value);

			// make sure it's there
			var item = cache.Get(Key);
			Assert.IsNotNull(item, "item just added is not there");

			// remove it
			cache.Remove(Key);

			// make sure it's not there
			item = cache.Get(Key);
			Assert.IsNull(item, "item still exists in cache");
		}

		[TestMethod]
		public void TestClear()
		{
			var cache = provider.BuildCache(RegionName, props);
			Assert.IsNotNull(cache, "no cache returned");

			// add the item
			cache.Put(Key, Value);

			// make sure it's there
			object item = cache.Get(Key);
			Assert.IsNotNull(item, "couldn't find item in cache");

			// clear the cache
			cache.Clear();

			// make sure we don't get an item
			item = cache.Get(Key);
			Assert.IsNull(item, "item still exists in cache");
		}

		[TestMethod]
		public void TestDefaultConstructor()
		{
			ICache cache = new MemoryCache();
			Assert.IsNotNull(cache);
		}

		[TestMethod]
		public void TestNoPropertiesConstructor()
		{
			ICache cache = new MemoryCache(RegionName);
			Assert.IsNotNull(cache);
		}

		[TestMethod]
		public void TestPriorityOutOfRange()
		{
			var h = new Dictionary<string, string> { { "priority", 7.ToString(CultureInfo.InvariantCulture) } };

			Executing
				.This(() => new MemoryCache(RegionName, h))
				.Should().Throw<IndexOutOfRangeException>();
		}

		[TestMethod]
		public void TestBadRelativeExpiration()
		{
			var h = new Dictionary<string, string> { { "expiration", "foobar" } };

			Executing
				.This(() => new MemoryCache(RegionName, h))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void TestEmptyProperties()
		{
			ICache cache = new MemoryCache(RegionName, new Dictionary<string, string>());
			Assert.IsNotNull(cache);
		}

		[TestMethod]
		public void TestNullKeyPut()
		{
			ICache cache = new MemoryCache();

			Executing
				.This(() => cache.Put(null, null))
				.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void TestNullValuePut()
		{
			ICache cache = new MemoryCache();

			Executing
				.This(() => cache.Put(RegionName, null))
				.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void TestNullKeyGet()
		{
			ICache cache = new MemoryCache();
			cache.Put(Key, "value");
			var item = cache.Get(null);

			Assert.IsNull(item);
		}

		[TestMethod]
		public void TestNullKeyRemove()
		{
			ICache cache = new MemoryCache();

			Executing
				.This(() => cache.Remove(null))
				.Should().Throw<ArgumentNullException>();
		}

		[TestMethod]
		public void TestRegions()
		{
			var cache1 = provider.BuildCache("nunit1", props);
			var cache2 = provider.BuildCache("nunit2", props);
			const string S1 = "test1";
			const string S2 = "test2";
			cache1.Put(Key, S1);
			cache2.Put(Key, S2);
			var get1 = cache1.Get(Key);
			var get2 = cache2.Get(Key);

			(get1 == get2).Should().Be.False();
		}

		[TestMethod]
		public void TestNonEqualObjectsWithEqualHashCodeAndToString()
		{
			var obj1 = new SomeObject();
			var obj2 = new SomeObject();

			obj1.Id = 1;
			obj2.Id = 2;

			var cache = provider.BuildCache("nunit", props);

			Assert.IsNull(cache.Get(obj2));
			cache.Put(obj1, obj1);
			Assert.AreEqual(obj1, cache.Get(obj1));
			Assert.IsNull(cache.Get(obj2));
		}

		[TestMethod]
		public void TestObjectExpiration()
		{
			var obj = new SomeObject { Id = 2 };

			var localProps =
				new Dictionary<string, string>
					{
						{ "expiration", ExpirySeconds.ToString(CultureInfo.InvariantCulture) }
					};

			var cache = provider.BuildCache(RegionName, localProps);

			Assert.IsNull(cache.Get(obj));
			cache.Put(Key, obj);

			// Wait
			Thread.Sleep(TimeSpan.FromSeconds(ExpirySeconds + 2));

			// Check it expired
			Assert.IsNull(cache.Get(Key));
		}

		[TestMethod]
		public void TestObjectExpirationAfterUpdate()
		{
			var obj = new SomeObject { Id = 2 };

			var localProps =
				new Dictionary<string, string>
					{
						{ "expiration", ExpirySeconds.ToString(CultureInfo.InvariantCulture) }
					};

			var cache = provider.BuildCache(RegionName, localProps);

			Assert.IsNull(cache.Get(obj));
			cache.Put(Key, obj);

			// This forces an object update
			cache.Put(Key, obj);

			// Wait
			Thread.Sleep(TimeSpan.FromSeconds(ExpirySeconds + 2));

			// Check it expired
			Assert.IsNull(cache.Get(Key));
		}

		private class SomeObject
		{
			public int Id
			{
				get;
				set;
			}

			public override int GetHashCode()
			{
				return 1;
			}

			public override string ToString()
			{
				return "TestObject";
			}

			public override bool Equals(object obj)
			{
				var other = obj as SomeObject;

				if (other == null)
				{
					return false;
				}

				return other.Id == Id;
			}
		}
	}
}