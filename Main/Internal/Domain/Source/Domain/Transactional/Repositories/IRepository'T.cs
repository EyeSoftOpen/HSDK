namespace EyeSoft.Domain.Transactional.Repositories
{
    using Aggregates;

    public interface IRepository<T> : IWriteRepository<T>, IReadOnlyRepository<T> where T : class, IAggregate
	{
	}
}