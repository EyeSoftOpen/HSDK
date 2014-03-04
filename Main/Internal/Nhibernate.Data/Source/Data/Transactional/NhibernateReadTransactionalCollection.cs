namespace EyeSoft.Data.Nhibernate.Transactional
{
	using System;
	using System.Linq;

	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	using NHibernate;
	using NHibernate.Linq;

	public class NhibernateReadTransactionalCollection : IReadTransactionalCollection
	{
		protected readonly ISession session;

		public NhibernateReadTransactionalCollection(ISession session)
		{
			Enforce.Argument(() => session);

			this.session = session;

			NhibernateSessionTracker.Open(session);
		}

		public IQueryable<T> Query<T>() where T : class, IAggregate
		{
			return session.Query<T>().Cacheable();
		}

		public T Load<T>(IComparable key) where T : class, IAggregate
		{
			Enforce.Argument(() => key);

			return session.Get<T>(key);
		}

		public void Dispose()
		{
			NhibernateSessionTracker.Remove(session);

			session.Dispose();
		}
	}
}