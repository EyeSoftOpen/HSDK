namespace EyeSoft.Data.Nhibernate.Caching.Memory
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.Text;

	using NHibernate;
	using NHibernate.Cache;

	/// <summary>
	/// Cache provider using the System.Runtime.Caching classes.
	/// </summary>
	public class MemoryCacheProvider : ICacheProvider
	{
		private static readonly Dictionary<string, ICache> caches;
		private static readonly IInternalLogger log;

		static MemoryCacheProvider()
		{
			log = LoggerProvider.LoggerFor(typeof(MemoryCacheProvider));
			caches = new Dictionary<string, ICache>();

			var list = ConfigurationManager.GetSection("memorycache") as CacheConfig[];

			if (list == null)
			{
				return;
			}

			foreach (var cache in list)
			{
				caches.Add(cache.Region, new MemoryCache(cache.Region, cache.Properties));
			}
		}

		public ICache BuildCache(string regionName, IDictionary<string, string> properties)
		{
			if (regionName == null)
			{
				regionName = string.Empty;
			}

			ICache result;

			if (caches.TryGetValue(regionName, out result))
			{
				return result;
			}

			// create cache
			if (properties == null)
			{
				properties = new Dictionary<string, string>(1);
			}

			if (log.IsDebugEnabled)
			{
				var sb = new StringBuilder();
				sb.Append("building cache with region: ").Append(regionName).Append(", properties: ");

				foreach (var de in properties)
				{
					sb.Append("name=");
					sb.Append(de.Key);
					sb.Append("&value=");
					sb.Append(de.Value);
					sb.Append(";");
				}

				log.Debug(sb.ToString());
			}

			return new MemoryCache(regionName, properties);
		}

		public long NextTimestamp()
		{
			return Timestamper.Next();
		}

		public void Start(IDictionary<string, string> properties)
		{
		}

		public void Stop()
		{
		}
	}
}