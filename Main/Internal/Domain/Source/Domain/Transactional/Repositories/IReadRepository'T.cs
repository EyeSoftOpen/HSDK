namespace EyeSoft.Domain.Transactional.Repositories
{
    using System.Linq;
    using Aggregates;

    public interface IReadOnlyRepository<out T> : IQueryable<T>, ILoadRepository<T> where T : class, IAggregate
	{
	}
}