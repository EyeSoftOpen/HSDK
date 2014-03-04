namespace EyeSoft.Collections.Generic
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Mapping;

	public static class CollectionExtensions
	{
		public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
		{
			Enforce.Argument(() => collection).Argument(() => items);

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

		public static ICollection<TFirst> Synchronize<TFirst, TSecond>(
			this ICollection<TFirst> first,
			IEnumerable<TSecond> second,
			Func<TSecond, TFirst> convert = null,
			Func<TFirst, int> firstHash = null,
			Func<TSecond, int> secondHash = null)
		{
			if (firstHash == null)
			{
				firstHash = x => x.GetHashCode();
			}

			if (secondHash == null)
			{
				secondHash = x => x.GetHashCode();
			}

			if (convert == null)
			{
				convert = x => Mapper.Map<TFirst>(x);
			}

			var firstCollection = first.ToDictionary(x => firstHash(x), x => x);
			var secondCollection = second.ToDictionary(x => secondHash(x), x => x);

			var toAdd = secondCollection.Where(item => firstCollection.All(x => x.Key != item.Key)).Select(x => convert(x.Value));

			foreach (var item in toAdd)
			{
				first.Add(item);
			}

			var toRemove = firstCollection.Where(item => secondCollection.All(x => x.Key != item.Key));

			foreach (var item in toRemove)
			{
				first.Remove(item.Value);
			}

			return first;
		}
	}
}