namespace EyeSoft.Transfer.Chunking.Domain.Repositories
{
	using System.Linq;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Transfer.Chunking.Domain.Aggregates;

	public class DocumentRepository
	{
		private readonly IRepository<Document> repository;

		public DocumentRepository(IRepository<Document> repository)
		{
			this.repository = repository;
		}

		public Document GetByPath(string path)
		{
			return repository.SingleOrDefault(document => document.Path == path);
		}

		public void Save(Document document)
		{
			repository.Save(document);
		}
	}
}