namespace EyeSoft.Transfer.Service.Test.Chunking
{
	using EyeSoft.Domain.Transactional;

	internal class MemoryTransactionalCollectionFactory : ITransactionalCollectionFactory
	{
		public ITransactionalCollection Create()
		{
			throw new System.NotImplementedException();
		}
	}
}