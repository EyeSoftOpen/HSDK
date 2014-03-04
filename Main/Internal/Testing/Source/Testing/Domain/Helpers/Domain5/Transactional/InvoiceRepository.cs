namespace EyeSoft.Testing.Domain.Helpers.Domain5.Transactional
{
	using System.Linq;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Domain.Transactional.Implementations;

	public class InvoiceRepository : Repository<Invoice>
	{
		private readonly IRepository<Invoice> repository;

		public InvoiceRepository(IRepository<Invoice> repository)
			: base(repository)
		{
			this.repository = repository;
		}

		public override void Save(Invoice invoice)
		{
			repository.Save(invoice);
		}

		public Invoice GetByCustomer(string customerName)
		{
			return this.SingleOrDefault(invoice => invoice.Customer == customerName);
		}
	}
}