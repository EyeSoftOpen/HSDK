namespace EyeSoft.Data.EntityFramework.Caching
{
	using System.Threading;

	using EyeSoft.Runtime.Caching;

	public static class DefaultCache
	{
		private static readonly object lockObject = new object();

		private static ICacheWithStatistics cacheInstance;

		private static ICache cacheFactory;

		public static ICacheWithStatistics Instance
		{
			get
			{
				if (cacheInstance == null)
				{
					lock (lockObject)
					{
						Thread.MemoryBarrier();

						if (cacheInstance == null)
						{
							cacheInstance =
								cacheFactory == null ?
									new EntityFrameworkCache() :
									new EntityFrameworkCache(new MemoryCache());
						}
					}
				}

				return cacheInstance;
			}
		}

		public static ICacheWithStatistics Register(ICache cacheFactory)
		{
			DefaultCache.cacheFactory = cacheFactory;

			return Instance;
		}
	}
}