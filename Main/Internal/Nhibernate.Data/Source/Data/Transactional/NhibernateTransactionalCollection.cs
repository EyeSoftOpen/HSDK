namespace EyeSoft.Data.Nhibernate.Transactional
{
	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;
	using EyeSoft.Normalization;

	using NHibernate;

	public class NhibernateTransactionalCollection : NhibernateReadTransactionalCollection, ITransactionalCollection
	{
		public NhibernateTransactionalCollection(ISession session) : base(session)
		{
		}

		public void Save<T>(T entity) where T : class, IAggregate
		{
			Enforce.Argument(() => entity);

			Ensure.That(entity.Id).Is.Not.Default();

			Normalizer.Normalize(entity);

			session.Merge(entity);
		}

		public void Delete<T>(T entity) where T : class, IAggregate
		{
			Enforce.Argument(() => entity);

			Ensure.That(entity.Id).Is.Not.Default();

			var entityToDelete = session.Load<T>(entity.Id);

			session.Delete(entityToDelete);
		}

		public void Commit()
		{
			using (var transaction = session.BeginTransaction())
			{
				session.Flush();

				transaction.Commit();
			}
		}
	}
}