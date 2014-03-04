namespace EyeSoft.Data.Nhibernate.Caching.Memory
{
	using System.Collections.Generic;
	using System.Configuration;
	using System.Xml;

	public class MemoryCacheSectionHandler : IConfigurationSectionHandler
	{
		public object Create(object parent, object configContext, XmlNode section)
		{
			var caches = new List<CacheConfig>();

			var nodes = section.SelectNodes("cache");

			foreach (XmlNode node in nodes)
			{
				string region = null;
				string expiration = null;
				var priority = "3";

				var regionAttribute = node.Attributes["region"];
				var expirationAttribute = node.Attributes["expiration"];
				var priorityAttribute = node.Attributes["priority"];

				if (regionAttribute != null)
				{
					region = regionAttribute.Value;
				}

				if (expirationAttribute != null)
				{
					expiration = expirationAttribute.Value;
				}

				if (priorityAttribute != null)
				{
					priority = priorityAttribute.Value;
				}

				if (region != null && expiration != null)
				{
					caches.Add(new CacheConfig(region, expiration, priority));
				}
			}

			return caches.ToArray();
		}
	}
}