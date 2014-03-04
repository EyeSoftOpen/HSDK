namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	using EyeSoft.Domain.Transactional;

	public class SchoolReadOnlyUnitOfWork : ReadOnlyUnitOfWork
	{
		public SchoolReadOnlyUnitOfWork(IReadTransactionalCollection readTransactionalCollection) : base(readTransactionalCollection)
		{
			CustomerOnlyRepository = ReadRepository<School>();
		}

		public IReadOnlyRepository<School> CustomerOnlyRepository { get; private set; }
	}
}