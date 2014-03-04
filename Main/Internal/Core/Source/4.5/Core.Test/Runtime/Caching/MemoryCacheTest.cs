namespace EyeSoft.Test.Runtime.Caching
{
	using EyeSoft.Runtime.Caching;
	using EyeSoft.Test.Runtime.Caching.Helpers;

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