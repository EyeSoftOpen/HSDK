namespace EyeSoft.EntityFramework.Caching.Demo.Mvc.Models
{
	using System.Collections.Generic;

	public class HomeViewModel
	{
		public HomeViewModel(IEnumerable<CustomerModel> customers, int cacheHits, int cacheMisses, int cacheAdds)
		{
			Customers = customers;
			CacheAdds = cacheAdds;
			CacheHits = cacheHits;
			CacheMisses = cacheMisses;
		}

		public IEnumerable<CustomerModel> Customers { get; set; }

		public string SearchTerm { get; set; }

		public int CacheHits { get; private set; }

		public int CacheMisses { get; private set; }

		public int CacheAdds { get; private set; }
	}
}