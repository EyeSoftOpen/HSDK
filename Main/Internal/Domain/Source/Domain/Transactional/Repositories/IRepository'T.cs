namespace EyeSoft.Domain.Transactional
{
	public interface IRepository<T> : IWriteRepository<T>, IReadOnlyRepository<T> where T : class, IAggregate
	{
	}
}