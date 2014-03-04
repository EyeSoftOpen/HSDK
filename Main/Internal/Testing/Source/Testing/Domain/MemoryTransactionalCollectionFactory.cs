namespace EyeSoft.Testing.Domain
{
	using EyeSoft.Domain.Transactional;

	public class MemoryTransactionalCollectionFactory
		: ITransactionalCollectionFactory
	{
		private ITransactionalCollection collection;

		public ITransactionalCollection Create()
		{
			return
				collection ??
				(collection = new MemoryTransactionalCollection());
		}
	}
}