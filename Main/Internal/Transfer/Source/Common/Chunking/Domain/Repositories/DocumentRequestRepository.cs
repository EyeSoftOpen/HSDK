namespace EyeSoft.Transfer.Chunking.Domain.Repositories
{
	using System;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Transfer.Chunking.Domain.Aggregates;

	public class DocumentRequestRepository
	{
		private readonly IRepository<DocumentRequest> repository;

		public DocumentRequestRepository(IRepository<DocumentRequest> repository)
		{
			this.repository = repository;
		}

		public void Add(DocumentRequest documentRequest)
		{
			repository.Save(documentRequest);
		}

		public DocumentRequest Load(Guid id)
		{
			return repository.Load(id);
		}
	}
}