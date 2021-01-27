namespace EyeSoft.Domain.Transactional
{
	using System;
	using System.Linq;
    using Aggregates;

    public interface IReadTransactionalCollection : IDisposable
	{
		IQueryable<T> Query<T>() where T : class, IAggregate;

		T Load<T>(IComparable key) where T : class, IAggregate;
	}
}