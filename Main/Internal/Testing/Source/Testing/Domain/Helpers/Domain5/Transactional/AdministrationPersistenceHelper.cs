namespace EyeSoft.Testing.Domain.Helpers.Domain5.Transactional
{
	using System;

	using EyeSoft.Data.Common;

	using SharpTestsEx;

	public static class AdministrationPersistenceHelper
	{
		public static void Check(Func<AdministrationUnitOfWork> createUnitOfWork, IDatabaseProvider databaseProvider = null)
		{
			if (databaseProvider != null)
			{
				databaseProvider.DropIfExists();
			}

			const string CustomerName = "Bill";

			using (var unitOfWork = createUnitOfWork())
			{
				unitOfWork.InvoiceRepository.Save(InvoiceFactory.ByCustomer(CustomerName, "12345"));
				unitOfWork.Commit();
			}

			using (var unitOfWork = createUnitOfWork())
			{
				var invoice = unitOfWork.InvoiceRepository.GetByCustomer(CustomerName);
				invoice.Customer.Should().Be.EqualTo(CustomerName);
			}
		}
	}
}