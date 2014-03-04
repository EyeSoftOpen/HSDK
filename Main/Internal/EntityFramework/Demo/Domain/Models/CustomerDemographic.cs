namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System.Collections.Generic;

	public class CustomerDemographic
	{
		public CustomerDemographic()
		{
			Customers = new List<Customer>();
		}

		public string CustomerTypeID { get; set; }

		public string CustomerDesc { get; set; }

		public virtual ICollection<Customer> Customers { get; set; }
	}
}