namespace EyeSoft.Data.EntityFramework.Test.Caching.Helpers.Northwind
{
	using System;
	using System.Data.Entity.Config;
	using System.Linq;

	using EyeSoft.Data.EntityFramework.Caching;
	using EyeSoft.EntityFramework.Caching.Demo.Domain;
	using EyeSoft.Runtime.Caching;
	using EyeSoft.Testing.Data;

	internal static class NorthwindHelper
	{
		private static readonly string connectionString = ConnectionString.Get("EntityFramework.Northwind");

		private static EntityFrameworkCache entityFrameworkCache;

		public static ICacheWithStatistics EntityFrameworkCache
		{
			get { return entityFrameworkCache; }
		}

		public static bool CacheEnabled
		{
			get { return entityFrameworkCache != null; }
		}

		public static void InitializeDb()
		{
			using (var unitOfWork = CreateUnitOfWork())
			{
				unitOfWork.Database.CreateIfNotExists();

				var dbSet = unitOfWork.Set<Category>();
				dbSet.ToList().ForEach(c => dbSet.Remove(c));
				unitOfWork.SaveChanges();
			}
		}

		public static NorthwindContext CreateUnitOfWork()
		{
			return new NorthwindContext(connectionString);
		}

		public static void EnableCache<TCacheFactory>()
			where TCacheFactory : ICacheFactory, new()
		{
			EnableCache(new TCacheFactory());
		}

		public static void EnableCache<TCacheFactory>(TCacheFactory cacheFactory) where TCacheFactory : ICacheFactory
		{
			if (entityFrameworkCache != null)
			{
				new InvalidOperationException("Cannot call the EnableCache method more than one time.")
					.Throw();
			}

			var cacheImplementation = cacheFactory.Create();
			cacheImplementation.Clear();

			entityFrameworkCache = new EntityFrameworkCache(cacheImplementation);

			DbConfiguration.SetConfiguration(new CachedDbConfiguration(entityFrameworkCache));
		}
	}
}