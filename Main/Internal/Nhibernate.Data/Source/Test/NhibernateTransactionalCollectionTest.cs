namespace EyeSoft.Data.Nhibernate.Test
{
	using EyeSoft.Data.Common;
	using EyeSoft.Data.Nhibernate;
	using EyeSoft.Data.Nhibernate.Test.Helpers;
	using EyeSoft.Data.Nhibernate.Test.Helpers.Mapping;
	using EyeSoft.Data.Nhibernate.Transactional;
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Testing.Domain;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class NhibernateTransactionalCollectionTest
		: TransactionalCollectionTest
	{
		private static NhibernateHelper helper;

		private static IDatabaseProvider databaseProvider;

		[ClassInitialize]
		public static void Initialize(TestContext context)
		{
			databaseProvider = DataProviderHelper.Create(DbNames.UnitOfWork);
			var mapping = new DomainMapper().Create(typeof(SchoolMapper));

			helper =
				new NhibernateHelper(databaseProvider, mapping);
		}

		[TestInitialize]
		public override void TestInitialize()
		{
			databaseProvider.DropIfExistsAndCreate();

			helper.Create();
		}

		[TestMethod]
		public override void AddEntityAndGet()
		{
			base.AddEntityAndGet();
		}

		[TestMethod]
		public override void ReadEntityWithOneToMany()
		{
			base.ReadEntityWithOneToMany();
		}

		[TestMethod]
		public override void ReadEntityWithOneToManyLaterAdded()
		{
			base.ReadEntityWithOneToManyLaterAdded();
		}

		[TestMethod]
		public override void ReadEntityWithOneToOne()
		{
			base.ReadEntityWithOneToOne();
		}

		protected override ITransactionalCollection CreateTransactionalCollection()
		{
			return new NhibernateTransactionalCollection(helper.OpenSession());
		}
	}
}