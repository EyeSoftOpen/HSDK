namespace EyeSoft.Domain.Transactional
{
	using System;

	public interface ILoadRepository<out T> where T : class, IAggregate
	{
		T Load(IComparable key);
	}
}