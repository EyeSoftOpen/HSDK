namespace EyeSoft.Data.EntityFramework.Caching
{
	using System;
	using System.Data.Common;
	using System.Data.Entity.Infrastructure;

	using EyeSoft.Data.EntityFramework.Toolkit;

	public class CachedContextConnectionFactory
		: IDbConnectionFactory
	{
		private readonly IEntityFrameworkCache cache;

		private readonly string providerName;

		static CachedContextConnectionFactory()
		{
			DbProviderFactoryBase.RegisterProvider<CachingProviderFactory>();
		}

		public CachedContextConnectionFactory()
			: this("System.Data.SqlClient")
		{
		}

		public CachedContextConnectionFactory(string providerName)
			: this(new EntityFrameworkCache(), providerName)
		{
		}

		public CachedContextConnectionFactory(
			IEntityFrameworkCache cache,
			string providerName = "System.Data.SqlClient")
		{
			if (cache == null)
			{
				throw new ArgumentNullException("cache");
			}

			if (providerName == null)
			{
				throw new ArgumentNullException("providerName");
			}

			this.cache = cache;
			this.providerName = providerName;
		}

		public DbConnection CreateConnection(string nameOrConnectionString)
		{
			if (nameOrConnectionString == null)
			{
				throw new ArgumentNullException("nameOrConnectionString");
			}

			var wrappedConnectionString = "wrappedProvider=" +
				providerName + ";" +
				nameOrConnectionString;

			var cachingConnection =
				new CachingConnection()
				{
					ConnectionString = wrappedConnectionString,
					CachingPolicy = CachingPolicy.CacheAll,
					Cache = cache
				};

			return cachingConnection;
		}
	}
}