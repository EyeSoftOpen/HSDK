namespace EyeSoft.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public static class EnumerableExtensions
    {
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            var list = new List<T>();

            foreach (var item in collection)
            {
                action(item);
                list.Add(item);
            }

            return list;
        }

        public static IEnumerable<T> Traverse<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> recursion)
        {
            foreach (var item in source)
            {
                yield return item;

                var recursionSequence = recursion(item);

                if (recursionSequence == null)
                {
                    continue;
                }

                foreach (var itemRecurse in Traverse(recursionSequence, recursion))
                {
                    yield return itemRecurse;
                }
            }
        }

        public static ReadOnlyCollection<T> AsReadOnly<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return null;
            }

            if (collection is List<T>)
            {
                return ((List<T>)collection).AsReadOnly();
            }

            if (collection is ReadOnlyCollection<T>)
            {
                return (ReadOnlyCollection<T>)collection;
            }

            return collection.ToList().AsReadOnly();
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return null;
            }

            if (collection is ObservableCollection<T>)
            {
                return (ObservableCollection<T>)collection;
            }

            return
                new ObservableCollection<T>(collection);
        }

        public static ReadOnlyObservableCollection<T> ToReadOnlyObservableCollection<T>(this IEnumerable<T> collection)
        {
            if (collection == null)
            {
                return null;
            }

            if (collection is ReadOnlyObservableCollection<T>)
            {
                return (ReadOnlyObservableCollection<T>)collection;
            }

            return
                new ReadOnlyObservableCollection<T>(collection.ToObservableCollection());
        }

        public static void Iterate<T>(this IEnumerable<T> collection)
        {
            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
            }
        }

        public static void Iterate<T>(this IEnumerable<T> collection, Action action)
        {
            var enumerator = collection.GetEnumerator();

            while (enumerator.MoveNext())
            {
                action();
            }
        }

        public static IEnumerable<TKey> DistinctBy<TSource, TKey>(
            this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            return
                source
                    .GroupBy(keySelector)
                    .Select(group => group.Key);
        }

        public static bool ContainsSequence<T>(this IEnumerable<T> superset, IEnumerable<T> subset, IEqualityComparer<T> comparer = null)
        {
            if (comparer == null)
            {
                return !subset.Except(superset).Any();
            }

            return !subset.Except(superset, comparer).Any();
        }

        public static bool Empty<T>(this IEnumerable<T> source)
        {
            return
                !source.Any();
        }

        public static bool Empty<T>(this IEnumerable<T> source, Func<T, bool> predicate)
        {
            return
                !source.Any(predicate);
        }
    }
}