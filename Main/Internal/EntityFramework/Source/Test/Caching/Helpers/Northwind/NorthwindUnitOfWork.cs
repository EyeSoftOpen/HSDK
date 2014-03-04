namespace EyeSoft.Data.EntityFramework.Test.Caching.Helpers.Northwind
{
	using EyeSoft.Domain.Transactional;

	internal class NorthwindUnitOfWork : UnitOfWork
	{
		public NorthwindUnitOfWork(ITransactionalCollection transactionalCollection) : base(transactionalCollection)
		{
		}

		public CategoryRepository CategoryRepository { get; private set; }
	}
}