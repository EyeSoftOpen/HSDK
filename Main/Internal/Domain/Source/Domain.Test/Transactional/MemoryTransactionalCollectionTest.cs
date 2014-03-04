namespace EyeSoft.Domain.Test.Transactional
{
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Testing.Domain;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class MemoryTransactionalCollectionTest
		: TransactionalCollectionTest
	{
		private ITransactionalCollection collection;

		[TestInitialize]
		public void Initialize()
		{
			collection = new MemoryTransactionalCollection();
		}

		[TestMethod]
		public override void AddEntityAndGet()
		{
			base.AddEntityAndGet();
		}

		[TestMethod]
		public override void ReadEntityWithOneToOne()
		{
			base.ReadEntityWithOneToOne();
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

		protected override ITransactionalCollection CreateTransactionalCollection()
		{
			return collection;
		}
	}
}