namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mapping;

    public static class CollectionExtensions
	{
		public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			var list = collection as List<T>;

			if (list != null)
			{
				list.AddRange(items);
				return list;
			}

			foreach (var item in items)
			{
				collection.Add(item);
			}

			return collection;
		}

		public static ICollection<T> Synchronize<T>(this ICollection<T> first, IEnumerable<T> second)
		{
			return first.Synchronize(second, x => x);
		}

		public static ICollection<T> Synchronize<T>(this ICollection<T> first, IEnumerable<T> second, Action<T> remove)
		{
			return first.Synchronize(second, x => x, null, remove);
		}

		public static ICollection<TFirst> Synchronize<TFirst, TSecond>(
			this ICollection<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TSecond, TFirst> convert = null,
			Func<TFirst, TSecond, bool> @equals = null,
			Action<TFirst> remove = null)
		{
			if (@equals == null)
			{
				@equals = (x, y) => x.Equals(y);
			}

			if (convert == null)
			{
				convert = x => Mapper.Map<TFirst>(x);
			}

			var firstCollection = first.ToArray();

			var secondCollection = second.ToArray();

			var toAdd = secondCollection.Where(item => firstCollection.All(x => !@equals(x, item))).Select(x => convert(x));

			foreach (var item in toAdd)
			{
				first.Add(item);
			}

			var toRemove = firstCollection.Where(item => secondCollection.All(x => !equals(item, x)));

			foreach (var item in toRemove)
			{
				if (remove == null)
				{
					first.Remove(item);
				}
				else
				{
					remove(item);
				}
			}

			return first;
		}
	}
}