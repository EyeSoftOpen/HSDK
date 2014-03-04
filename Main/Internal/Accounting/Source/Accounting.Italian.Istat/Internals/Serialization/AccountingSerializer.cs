namespace EyeSoft.Accounting.Italian.Istat
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Web.Script.Serialization;

	using EyeSoft.Reflection;

	internal static class AccountingSerializer
	{
		private static readonly object lockInstance = new object();

		public static ReadOnlyCollection<T> Collection<TSerializable, T>(
			ref ReadOnlyCollection<T> collection, Func<TSerializable, T> convert) where T : class
		{
			lock (lockInstance)
			{
				if (collection != null)
				{
					return collection;
				}

				var resourceName = string.Format("Internals.{0}.json.gz", typeof(T).Name);

				var decompressed = typeof(AccountingSerializer).Assembly.ReadGzipResourceText(resourceName);

				var serializer = new JavaScriptSerializer();
				var deserialized = serializer.Deserialize<IEnumerable<TSerializable>>(decompressed);

				var list = deserialized.Select(convert).ToList();

				collection = new ReadOnlyCollection<T>(list);

				return collection;
			}
		}
	}
}