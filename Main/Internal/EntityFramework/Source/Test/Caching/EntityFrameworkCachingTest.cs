namespace EyeSoft.Data.EntityFramework.Test.Caching
{
	using System;
	using System.Diagnostics;
	using System.Linq;

	using EyeSoft.Data.EntityFramework.Test.Caching.Helpers.Northwind;
	using EyeSoft.EntityFramework.Caching.Demo.Domain;
	using EyeSoft.Runtime.Caching;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EntityFrameworkCachingTest
	{
		private const int Readings = 10;

		[TestInitialize]
		public void TestInitiliaze()
		{
			NorthwindHelper.EnableCache<MemoryCacheFactory>();
			NorthwindHelper.InitializeDb();

			PopulateDb();
		}

		[TestMethod]
		public void CheckCacheHits()
		{
			var stopwatch = Stopwatch.StartNew();

			CheckHits(0, 0);

			for (var i = 0; i < Readings; i++)
			{
				CheckHits(i, i + 1);
			}

			stopwatch.Stop();

			Console.WriteLine("Total time with cache status {0}: {1}", NorthwindHelper.CacheEnabled, stopwatch.Elapsed);
		}

		private static void PopulateDb()
		{
			using (var unitOfWork = NorthwindHelper.CreateUnitOfWork())
			{
				if (NorthwindHelper.CacheEnabled)
				{
					NorthwindHelper.EntityFrameworkCache.Hits.Should().Be.EqualTo(0);
				}

				for (var index = 0; index < 10; index++)
				{
					unitOfWork.Set<Category>().Add(new Category { CategoryName = "Cat" + index });
				}

				unitOfWork.SaveChanges();
			}
		}

		private void CheckHits(int beforeRead, int afterRead)
		{
			using (var unitOfWork = NorthwindHelper.CreateUnitOfWork())
			{
				if (NorthwindHelper.CacheEnabled)
				{
					NorthwindHelper.EntityFrameworkCache.Hits.Should().Be.EqualTo(beforeRead);
				}

				var category =
					unitOfWork
						.Set<Category>()
						.Where(x => x.CategoryName.StartsWith("Ca"))
						.ToList();

				category.Should().Not.Be.Null();

				if (NorthwindHelper.CacheEnabled)
				{
					NorthwindHelper.EntityFrameworkCache.Hits.Should().Be.EqualTo(afterRead);
				}
			}
		}
	}
}