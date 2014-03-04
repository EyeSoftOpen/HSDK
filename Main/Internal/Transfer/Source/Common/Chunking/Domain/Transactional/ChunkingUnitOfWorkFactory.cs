namespace EyeSoft.Transfer.Chunking.Domain.Transactional
{
	using EyeSoft.Domain.Transactional;

	public class ChunkingUnitOfWorkFactory
	{
		private readonly ITransactionalCollectionFactory collectionFactory;

		public ChunkingUnitOfWorkFactory(ITransactionalCollectionFactory collectionFactory)
		{
			this.collectionFactory = collectionFactory;
		}

		public ChunkingUnitOfWork Create()
		{
			return new ChunkingUnitOfWork(collectionFactory.Create());
		}
	}
}