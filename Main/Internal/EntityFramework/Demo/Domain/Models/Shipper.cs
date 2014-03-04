namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System.Collections.Generic;

	public class Shipper
	{
		public Shipper()
		{
			Orders = new List<Order>();
		}

		public int ShipperId { get; set; }

		public string CompanyName { get; set; }

		public string Phone { get; set; }

		public virtual ICollection<Order> Orders { get; set; }
	}
}