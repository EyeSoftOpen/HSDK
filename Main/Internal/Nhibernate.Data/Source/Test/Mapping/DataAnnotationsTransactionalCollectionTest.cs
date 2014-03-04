namespace EyeSoft.Data.Nhibernate.Test.Mapping
{
	using EyeSoft.Data.Common;
	using EyeSoft.Data.Nhibernate;
	using EyeSoft.Data.Nhibernate.Mapping;
	using EyeSoft.Data.Nhibernate.Test.Helpers;
	using EyeSoft.Data.Nhibernate.Transactional;
	using EyeSoft.Testing.Domain.Helpers.Domain4;
	using EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class DataAnnotationsTransactionalCollectionTest
	{
		private static NhibernateHelper helper;

		private static IDatabaseProvider databaseProvider;

		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			databaseProvider = DataProviderHelper.Create(DbNames.Mapping);

			var mapping =
				new ModelAnnotationsMapper()
					.Map<Blog>()
					.Map<Post>()
					.CompileMapping();

			MappingHelper.Verify(mapping, "Mapping.Domain4.Mapping.xml");

			helper =
				new NhibernateHelper(databaseProvider, mapping);
		}

		[TestInitialize]
		public void TestInitialize()
		{
			databaseProvider.DropIfExists();

			helper.Create();
		}

		[TestMethod]
		public void AddAggregateAndCheck()
		{
			BloggerPersistenceHelper.Check(CreateUow);
		}

		protected BloggerUnitOfWork CreateUow()
		{
			return new BloggerUnitOfWork(new NhibernateTransactionalCollection(helper.OpenSession()));
		}
	}
}