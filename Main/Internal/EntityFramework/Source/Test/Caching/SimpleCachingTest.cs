namespace EyeSoft.Data.EntityFramework.Test.Caching
{
	using System;
	using System.Data.Entity.Config;
	using System.Linq;

	using EyeSoft.Data.EntityFramework.Caching;
	using EyeSoft.Testing.Data;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class SimpleCachingTest
	{
		private static readonly string connectionString = ConnectionString.Get("SimpleCaching");

		private static readonly EntityFrameworkCache entityFrameworkCache = new EntityFrameworkCache();

		[TestInitialize]
		public void TestInitialize()
		{
			DbConfiguration.SetConfiguration(new CachedDbConfiguration(entityFrameworkCache));
		}

		[TestMethod]
		public void CacheHitsWithDefaultDbContext()
		{
			using (var unitOfWork = new FinanceUnitOfWork(connectionString))
			{
				if (unitOfWork.Database.Exists())
				{
					unitOfWork.Database.Delete();
				}
			}

			using (var unitOfWork = new FinanceUnitOfWork(connectionString))
			{
				unitOfWork.CustomerRepository.Add(new Customer { Id = Guid.NewGuid(), Name = "Bill" });
				unitOfWork.SaveChanges();
			}

			Action readingAction = () =>
				{
					using (var unitOfWork = new FinanceUnitOfWork(connectionString))
					{
						var customers = unitOfWork.CustomerRepository.Where(customer => customer.Name.StartsWith("Bi")).ToList();
						customers.Should().Not.Be.Null();
					}
				};

			const int Readings = 4;

			Enumerable.Range(1, Readings).ToList().ForEach(d => readingAction());

			entityFrameworkCache.Hits.Should().Be.EqualTo(Readings - 1);
		}
	}
}