namespace EyeSoft.Data.EntityFramework.Test
{
	using EyeSoft.Data.EntityFramework.Test.Caching.Helpers.School;
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Testing.Domain;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class EntityFrameworkTransactionalCollectionTest : TransactionalCollectionTest
	{
		[TestInitialize]
		public override void TestInitialize()
		{
			SchoolHelper.InitializeDb();
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
			return SchoolHelper.CreateCollection();
		}
	}
}