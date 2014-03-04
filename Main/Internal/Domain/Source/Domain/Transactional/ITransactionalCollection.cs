namespace EyeSoft.Domain.Transactional
{
	public interface ITransactionalCollection : IReadTransactionalCollection
	{
		void Save<T>(T entity) where T : class, IAggregate;

		void Delete<T>(T entity) where T : class, IAggregate;

		void Commit();
	}
}