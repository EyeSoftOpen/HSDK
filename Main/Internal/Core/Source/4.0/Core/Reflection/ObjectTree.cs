namespace EyeSoft.Core.Reflection
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public static class ObjectTree
	{
		private static readonly IDictionary<string, EnumerablePropertyInfo> allPropertiesCache =
			new Dictionary<string, EnumerablePropertyInfo>();

		public static IEnumerable<object> Traverse(object obj, Action<object> process)
		{
			var referenceList = new HashSet<object>();

			Traverse(referenceList, obj, process);

			return referenceList;
		}

		private static void Traverse(ICollection<object> referenceList, object obj, Action<object> process)
		{
			if (obj == null)
			{
				return;
			}

			if (referenceList.Contains(obj))
			{
				return;
			}

			referenceList.Add(obj);

			process(obj);

			var properties = allPropertiesCache.GetProperties(obj.GetType(), PropertyPredicates.Reference);

			var children = properties.Select(x => x.GetValue(obj, null)).Where(o => o != null).ToArray();

			foreach (var child in children)
			{
				if (child is IEnumerable enumerable)
				{
					foreach (var item in enumerable)
					{
						Traverse(referenceList, item, process);
					}

					continue;
				}

				Traverse(referenceList, child, process);
			}
		}
	}
}