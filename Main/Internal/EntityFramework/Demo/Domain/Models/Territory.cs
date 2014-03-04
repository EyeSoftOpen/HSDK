namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System.Collections.Generic;

	public class Territory
	{
		public Territory()
		{
			Employees = new List<Employee>();
		}

		public string TerritoryId { get; set; }

		public string TerritoryDescription { get; set; }

		public int RegionId { get; set; }

		public virtual Region Region { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }
	}
}