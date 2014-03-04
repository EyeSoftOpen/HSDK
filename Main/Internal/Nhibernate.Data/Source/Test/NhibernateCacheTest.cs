namespace EyeSoft.Data.Nhibernate.Test
{
	using System;
	using System.Linq;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.Nhibernate.Caching.Memory;
	using EyeSoft.Data.Nhibernate.Test.Helpers;
	using EyeSoft.Data.Nhibernate.Test.Helpers.Mapping;
	using EyeSoft.Data.Nhibernate.Transactional;
	using EyeSoft.Diagnostic;
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using NHibernate.Cfg.Loquacious;

	using SharpTestsEx;

	[TestClass]
	public class NhibernateCacheTest
	{
		[TestMethod]
		public void VerifyNhibernateSecondLevelCacheIsWorking()
		{
			var databaseProvider = DataProviderHelper.Create(DbNames.Cache);
			databaseProvider.CreateIfNotExists();

			var mapping = new DomainMapper().Create(typeof(SchoolMapper));

			MappingHelper.Verify(mapping, "Helpers.Mapping.Domain1.Mapping.xml");

			var helper =
				new NhibernateHelper(databaseProvider, mapping, new CacheConfigurationApplier(SchemaAction.Create));

			helper.Statistics.IsStatisticsEnabled = true;

			using (var collection = new NhibernateTransactionalCollection(helper.OpenSession()))
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					unitOfWork.SchoolRepository.Save(Known.Schools.SchoolWithOneChild);
					unitOfWork.Commit();
				}
			}

			const int Reads = 10;

			var elapsed =
				Benchmark
					.For(Reads, () => GetAndCheckSchool(helper));

			Console.WriteLine("Elapsed time for {0} iterations is {1}", Reads, elapsed);

			helper
				.Statistics.SecondLevelCacheHitCount
				.Should().Be.EqualTo(Reads - 1);
		}

		private static void GetAndCheckSchool(NhibernateHelper helper)
		{
			using (var collection = new NhibernateTransactionalCollection(helper.OpenSession()))
			{
				using (var unitOfWork = new SchoolUnitOfWork(collection))
				{
					var school =
						unitOfWork.SchoolRepository.Single();

					school.Name
						.Should().Be.EqualTo(Known.Schools.SchoolWithOneChild.Name);
				}
			}
		}

		private class CacheConfigurationApplier : DefaultConfiguration
		{
			public CacheConfigurationApplier(SchemaAction schemaAction)
				: base(schemaAction)
			{
			}

			protected override void SetCache(ICacheConfigurationProperties cache)
			{
				cache.Provider<MemoryCacheProvider>();
				cache.UseQueryCache = true;
				cache.UseMinimalPuts = true;
			}
		}
	}
}