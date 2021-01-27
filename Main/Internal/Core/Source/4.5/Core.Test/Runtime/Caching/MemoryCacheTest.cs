namespace EyeSoft.Core.Test.Runtime.Caching
{
    using Core.Runtime.Caching;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
	public class MemoryCacheTest : CacheTest
	{
		[TestMethod]
		public override void InsertAKeyAndRetrieveVerifyTheValueIsTheSame()
		{
			base.InsertAKeyAndRetrieveVerifyTheValueIsTheSame();
		}

		protected override ICache Create()
		{
			return new MemoryCache();
		}
	}
}