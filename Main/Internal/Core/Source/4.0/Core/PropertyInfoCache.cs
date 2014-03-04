namespace EyeSoft
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;

	public static class PropertyInfoCache
	{
		private static readonly object lockInstance = new object();

		public static EnumerablePropertyInfo GetProperties(
			this IDictionary<string, EnumerablePropertyInfo> propertyCache, Type type, Lazy<PropertyPredicate> lazyPredicate)
		{
			var predicate = lazyPredicate.Value;
			return GetProperties(propertyCache, type, predicate);
		}

		public static EnumerablePropertyInfo GetProperties(
			this IDictionary<string, EnumerablePropertyInfo> propertyCache, Type type, PropertyPredicate predicate)
		{
			return GetProperties(propertyCache, type, predicate.Name, predicate.Predicate);
		}

		public static EnumerablePropertyInfo GetProperties(
			this IDictionary<string, EnumerablePropertyInfo> propertyCache, Type type, string key, Func<PropertyInfo, bool> predicate)
		{
			lock (lockInstance)
			{
				var fullKey = string.Concat(type.FullName, ".", key);

				if (propertyCache.ContainsKey(fullKey))
				{
					return propertyCache[fullKey];
				}

				var properties = type.GetAnyInstanceProperties().Where(predicate);

				var typeProperties = new EnumerablePropertyInfo(properties);

				propertyCache.Add(fullKey, typeProperties);

				return propertyCache[fullKey];
			}
		}
	}
}