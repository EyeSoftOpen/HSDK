namespace EyeSoft.Domain.Transactional.Repositories
{
    using System;
    using Aggregates;

    public interface ILoadRepository<out T> where T : class, IAggregate
	{
		T Load(IComparable key);
	}
}