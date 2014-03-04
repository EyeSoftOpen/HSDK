namespace EyeSoft.Testing.Domain.Helpers.Domain4.Trasactional
{
	using EyeSoft.Domain.Transactional;

	public class BloggerUnitOfWork : UnitOfWork
	{
		public BloggerUnitOfWork(ITransactionalCollection collection)
			: base(collection)
		{
		}

		public BlogRepository BlogRepository { get; private set; }
	}
}