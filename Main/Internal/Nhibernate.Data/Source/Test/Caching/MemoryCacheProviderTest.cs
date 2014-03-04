namespace EyeSoft.Data.Nhibernate.Test.Caching
{
	using System.Collections.Generic;
	using System.Globalization;

	using EyeSoft.Data.Nhibernate.Caching.Memory;

	using log4net.Config;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class MemoryCacheProviderTest
	{
		private static Dictionary<string, string> props;
		private MemoryCacheProvider provider;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			XmlConfigurator.Configure();
			props = new Dictionary<string, string>
				{
					{ "expiration", 120.ToString(CultureInfo.InvariantCulture) },
					{ "priority", 2.ToString(CultureInfo.InvariantCulture) }
				};
		}

		[TestInitialize]
		public void Setup()
		{
			provider = new MemoryCacheProvider();
		}

		[TestMethod]
		public void TestBuildCacheFromConfig()
		{
			var cache = provider.BuildCache("foo", null);
			Assert.IsNotNull(cache, "pre-configured cache not found");
		}

		[TestMethod]
		public void TestBuildCacheNullNull()
		{
			var cache = provider.BuildCache(null, null);
			Assert.IsNotNull(cache, "no cache returned");
		}

		[TestMethod]
		public void TestBuildCacheStringCollection()
		{
			var cache = provider.BuildCache("another_region", props);
			Assert.IsNotNull(cache, "no cache returned");
		}

		[TestMethod]
		public void TestBuildCacheStringNull()
		{
			var cache = provider.BuildCache("a_region", null);
			Assert.IsNotNull(cache, "no cache returned");
		}

		[TestMethod]
		public void TestNextTimestamp()
		{
			var ts = provider.NextTimestamp();
			Assert.IsNotNull(ts, "no timestamp returned");
		}
	}
}