namespace EyeSoft.Core.Test.Runtime.Caching.Helpers
{
    using System;
    using Core.Runtime.Caching;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public abstract class CacheTest : IDisposable
	{
		private ICache factory;

		[TestInitialize]
		public void TestInitialize()
		{
			factory = Create();
		}

		[TestCleanup]
		public void CleanUp()
		{
			factory.Dispose();
		}

		[TestMethod]
		public virtual void InsertAKeyAndRetrieveVerifyTheValueIsTheSame()
		{
			using (var cache = Create())
			{
				const string Key = "Test";
				const string Value = "Hello!";

				cache.Remove(Key);
				cache.Add(Key, Value);

				var valueRetrieved = cache.Get(Key);

				valueRetrieved.Should().Be.EqualTo(Value);
			}
		}

		public void Dispose()
		{
			using (factory)
			{
			}
		}

		protected abstract ICache Create();
	}
}