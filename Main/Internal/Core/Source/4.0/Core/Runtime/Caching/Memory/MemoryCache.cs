namespace EyeSoft.Core.Runtime.Caching
{
    using System;
    using System.Globalization;

    public class MemoryCache : MemoryCache<object>, ICache
	{
		public MemoryCache()
			: base(new System.Runtime.Caching.MemoryCache(DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture)))
		{
		}

		public MemoryCache(string name)
			: base(new System.Runtime.Caching.MemoryCache(name))
		{
		}

		public MemoryCache(System.Runtime.Caching.MemoryCache cache) : base(cache)
		{
		}
	}
}