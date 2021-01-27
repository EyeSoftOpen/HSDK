namespace EyeSoft.Core.Collections.Specialized
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;

    public static class NameValueCollectionExtensions
	{
		public static T Get<T>(this NameValueCollection collection, string key)
		{
			var value = collection[key];

			if (value == null)
			{
				throw new ArgumentException(string.Format("The key {0} is not defined into the configuration file.", key));
			}

			var converter = TypeDescriptor.GetConverter(typeof(T));

			var text = collection[key];
			var convertedValue = converter.ConvertFromString(text);

			return (T)convertedValue;
		}

		public static T GetOrDefault<T>(this NameValueCollection collection, string key, T defaultValue = default(T))
		{
			var value = collection[key];

			if (value == null)
			{
				return defaultValue;
			}

			var converter = TypeDescriptor.GetConverter(typeof(T));

			var convertedValue = converter.ConvertFromString(value);

			return (T)convertedValue;
		}
	}
}