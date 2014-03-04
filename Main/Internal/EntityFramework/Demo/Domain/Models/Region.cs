namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System.Collections.Generic;

	public class Region
	{
		public Region()
		{
			Territories = new List<Territory>();
		}

		public int RegionID { get; set; }

		public string RegionDescription { get; set; }

		public virtual ICollection<Territory> Territories { get; set; }
	}
}