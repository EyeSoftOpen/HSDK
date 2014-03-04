namespace EyeSoft.Domain.Transactional
{
	public interface IWriteRepository<T> : ILoadRepository<T> where T : class, IAggregate
	{
		void Save(T entity);

		bool Remove(T entity);

		void Commit();
	}
}