namespace EyeSoft.Windows.Model.Demo.Contract
{
	using System;
	using System.Collections.Generic;

	public interface ICustomerService : IDisposable
	{
		IEnumerable<CustomerDto> GetCustomersWithTurnoverGreatherThan(int turnover);

		void Save(CustomerDto customer);

		CustomerDto GetMainCustomer();

		void DeleteAll();

		void Delete(Guid id);
	}
}