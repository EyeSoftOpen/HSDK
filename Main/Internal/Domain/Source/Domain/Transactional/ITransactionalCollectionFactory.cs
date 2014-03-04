namespace EyeSoft.Domain.Transactional
{
	public interface ITransactionalCollectionFactory
	{
		ITransactionalCollection Create();
	}
}