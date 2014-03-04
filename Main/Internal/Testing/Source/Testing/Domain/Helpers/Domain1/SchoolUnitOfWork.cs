namespace EyeSoft.Testing.Domain.Helpers.Domain1
{
	using EyeSoft.Domain.Transactional;

	public class SchoolUnitOfWork
		: UnitOfWork
	{
		public SchoolUnitOfWork(ITransactionalCollection transactionalCollection)
			: base(transactionalCollection)
		{
		}

		public IRepository<School> SchoolRepository { get; private set; }

		public IReadOnlyRepository<Child> ChildOnlyRepository { get; private set; }
	}
}