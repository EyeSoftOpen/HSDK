namespace EyeSoft.Data.Raven
{
	using System;
	using System.Linq;

	using EyeSoft.Domain;
	using EyeSoft.Domain.Transactional;

	using global::Raven.Client;

	public class RavenReadTransactionalCollection : IReadTransactionalCollection
	{
		protected readonly IDocumentSession session;

		public RavenReadTransactionalCollection(IDocumentSession session)
		{
			this.session = session;
		}

		public void Dispose()
		{
			session.Dispose();
		}

		public IQueryable<T> Query<T>() where T : class, IAggregate
		{
			return session.Query<T>();
		}

		public T Load<T>(IComparable key) where T : class, IAggregate
		{
			return session.Load<T>(key.ToString());
		}
	}
}