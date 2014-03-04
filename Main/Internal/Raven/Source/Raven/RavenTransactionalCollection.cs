namespace EyeSoft.Data.Raven
{
	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	using global::Raven.Client;

	public class RavenTransactionalCollection : RavenReadTransactionalCollection, ITransactionalCollection
	{
		public RavenTransactionalCollection(IDocumentSession session) : base(session)
		{
		}

		public void Save<T>(T entity) where T : class, IAggregate
		{
			session.Store(entity);
		}

		public void Delete<T>(T entity) where T : class, IAggregate
		{
			session.Delete(entity);
		}

		public void Commit()
		{
			session.SaveChanges();
		}
	}
}