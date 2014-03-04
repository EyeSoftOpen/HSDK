namespace EyeSoft.Domain.Transactional
{
	using System.Linq;

	public interface IReadOnlyRepository<out T> : IQueryable<T>, ILoadRepository<T> where T : class, IAggregate
	{
	}
}