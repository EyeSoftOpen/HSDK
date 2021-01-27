namespace EyeSoft.Domain.Transactional
{
    using Aggregates;

    public interface ITransactionalCollection : IReadTransactionalCollection
	{
		void Save<T>(T entity) where T : class, IAggregate;

		void Delete<T>(T entity) where T : class, IAggregate;

		void Commit();
	}
}