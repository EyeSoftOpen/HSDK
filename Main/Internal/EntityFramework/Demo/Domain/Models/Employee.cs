namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System;
	using System.Collections.Generic;

	public class Employee
	{
		public Employee()
		{
			Employees1 = new List<Employee>();
			Orders = new List<Order>();
			Territories = new List<Territory>();
		}

		public int EmployeeId { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }

		public string Title { get; set; }

		public string TitleOfCourtesy { get; set; }

		public DateTime? BirthDate { get; set; }

		public DateTime? HireDate { get; set; }

		public string Address { get; set; }

		public string City { get; set; }

		public string Region { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }

		public string HomePhone { get; set; }

		public string Extension { get; set; }

		public byte[] Photo { get; set; }

		public string Notes { get; set; }

		public int? ReportsTo { get; set; }

		public string PhotoPath { get; set; }

		public virtual ICollection<Employee> Employees1 { get; set; }

		public virtual Employee Employee1 { get; set; }

		public virtual ICollection<Order> Orders { get; set; }

		public virtual ICollection<Territory> Territories { get; set; }
	}
}