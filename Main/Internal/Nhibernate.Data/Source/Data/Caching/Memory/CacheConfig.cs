namespace EyeSoft.Data.Nhibernate.Caching.Memory
{
	using System.Collections.Generic;

	public class CacheConfig
	{
		private readonly Dictionary<string, string> properties;
		private readonly string regionName;

		public CacheConfig(string region, string expiration, string priority)
		{
			regionName = region;
			properties = new Dictionary<string, string> { { "expiration", expiration }, { "priority", priority } };
		}

		public string Region
		{
			get { return regionName; }
		}

		public IDictionary<string, string> Properties
		{
			get { return properties; }
		}
	}
}