namespace EyeSoft.Testing.Domain.Helpers.Domain5.Transactional
{
	using EyeSoft.Domain.Transactional;

	public class AdministrationUnitOfWork : UnitOfWork
	{
		public AdministrationUnitOfWork(ITransactionalCollection collection) : base(collection)
		{
		}

		public InvoiceRepository InvoiceRepository { get; private set; }
	}
}