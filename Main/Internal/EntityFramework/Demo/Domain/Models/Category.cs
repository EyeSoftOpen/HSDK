namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System;
	using System.Collections.Generic;

	using EyeSoft.Domain;

	public class Category : IAggregate
	{
		public Category()
		{
			Products = new List<Product>();
		}

		public int CategoryId { get; set; }

		public string CategoryName { get; set; }

		public string Description { get; set; }

		public byte[] Picture { get; set; }

		public virtual ICollection<Product> Products { get; set; }

		public IComparable Id
		{
			get
			{
				return CategoryId;
			}
			set
			{
				CategoryId = (int)value;
			}
		}
	}
}