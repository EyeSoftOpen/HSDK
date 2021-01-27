namespace EyeSoft.Domain.Transactional.Repositories
{
    using Aggregates;

    public interface IWriteRepository<T> : ILoadRepository<T> where T : class, IAggregate
	{
		void Save(T entity);

		bool Remove(T entity);

		void Commit();
	}
}