namespace EyeSoft.Accounting.Italian.Istat.Internals.Serialization
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using Core.Reflection;
    using Newtonsoft.Json;

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

				var json = typeof(AccountingSerializer).Assembly.ReadGzipResourceText(resourceName);

				var list = JsonConvert.DeserializeObject<IEnumerable<TSerializable>>(json).Select(convert).ToList();

				collection = new ReadOnlyCollection<T>(list);

				return collection;
			}
		}
	}
}