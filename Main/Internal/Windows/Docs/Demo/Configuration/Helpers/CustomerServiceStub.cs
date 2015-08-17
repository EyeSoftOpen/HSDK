namespace EyeSoft.Wpf.Facilities.Demo.Configuration.Helpers
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading;

	using EyeSoft.Windows.Model.Demo.Configuration.Helpers;
	using EyeSoft.Windows.Model.Demo.Contract;

	internal class CustomerServiceStub : ICustomerService
	{
		private readonly bool runSlow;

		public CustomerServiceStub(bool runSlow = true)
		{
			this.runSlow = runSlow;
		}

		public bool ItemLoaded { get; private set; }

		public bool CollectionLoaded { get; private set; }

		public IEnumerable<CustomerDto> GetCustomersWithTurnoverGreatherThan(int turnover)
		{
			if (runSlow)
			{
				Thread.Sleep(3000);
			}

			var customersWithTurnoverGreatherThan = Known.Customer.All.Where(x => x.Turnover >= turnover).ToList();

			CollectionLoaded = true;

			return customersWithTurnoverGreatherThan;
		}

		public void Save(CustomerDto mainCustomer)
		{
		}

		public CustomerDto GetMainCustomer()
		{
			ItemLoaded = true;
			return Known.Customer.Main;
		}

		public void DeleteAll()
		{
		}

		public void Delete(Guid id)
		{
		}

		public void Dispose()
		{
		}
	}
}