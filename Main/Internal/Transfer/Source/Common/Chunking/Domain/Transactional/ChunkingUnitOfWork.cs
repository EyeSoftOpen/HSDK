namespace EyeSoft.Transfer.Chunking.Domain.Transactional
{
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Transfer.Chunking.Domain.Aggregates;
	using EyeSoft.Transfer.Chunking.Domain.Repositories;

	public class ChunkingUnitOfWork :
		UnitOfWork
	{
		public ChunkingUnitOfWork(ITransactionalCollection transactionalCollection)
			: base(transactionalCollection)
		{
			DocumentRepository = new DocumentRepository(Repository<Document>());
			DocumentRequestRepository = new DocumentRequestRepository(Repository<DocumentRequest>());
		}

		public DocumentRepository DocumentRepository { get; private set; }

		public DocumentRequestRepository DocumentRequestRepository { get; private set; }
	}
}