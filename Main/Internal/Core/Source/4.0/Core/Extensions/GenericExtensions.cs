namespace EyeSoft.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using Collections.Generic;

    public static class GenericsExtensions
    {
        public static object GetDefault(Type type)
        {
            return type.IsValueType ? Activator.CreateInstance(type) : null;
        }

        public static bool IsDefault<T>(this T obj)
        {
            var isDefault = EqualityComparer<T>.Default.Equals(obj, default(T));

            if (!isDefault)
            {
                return EqualityComparer.AreEquals(obj, GetDefault(obj.GetType()));
            }

            return true;
        }

        public static void OnNotDefault<T>(this IObjectExtender<T> obj, Action<T> action) where T : class
        {
            var instance = obj.Instance;

            if (instance.IsDefault())
            {
                return;
            }

            action(instance);
        }

        public static IEnumerable<object> GetEnumerable<T>(this IObjectExtender<T> obj)
        {
            var collection = obj.Instance as IEnumerable;

            if (collection == null)
            {
                yield return obj;
                yield break;
            }

            foreach (var item in collection)
            {
                yield return item;
            }
        }

        public static IEnumerable<object> GetFlatternChildren<T>(this IObjectExtender<T> root)
            where T : class
        {
            return GetFlatternChildren(root, type => type.IsPrimitiveType() || type.IsEnumerableOf<T>());
        }

        public static IEnumerable<object> GetFlatternChildren<T>(this IObjectExtender<T> root, Predicate<Type> predicate)
        {
            return GetFlatternChildren(root, new HashSet<object>(), predicate);
        }

        private static IEnumerable<object> GetFlatternChildren<T>(
            this IObjectExtender<T> root, ICollection<object> list, Predicate<Type> predicate)
        {
            var instance = root.Instance;

            if (list.Contains(instance))
            {
                return Enumerable.Empty<object>();
            }

            const BindingFlags InstanceMembers = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

            var properties = instance.GetType().GetProperties(InstanceMembers);
            var fields = instance.GetType().GetFields(InstanceMembers);

            var filteredProperties =
                properties
                    .Where(item => predicate(item.PropertyType))
                    .Select(item => item.GetValue(instance, null))
                    .SelectMany(item => item.Extend().GetEnumerable())
                    .Where(item => item != null && !item.Equals(instance));

            var filteredFields =
                fields
                    .Where(item => predicate(item.FieldType))
                    .Select(item => item.GetValue(instance))
                    .SelectMany(item => item.Extend().GetEnumerable())
                    .Where(item => item != null && !item.Equals(instance));

            AddChildren(list, predicate, filteredProperties);
            AddChildren(list, predicate, filteredFields);

            return list;
        }

        private static void AddChildren(
            ICollection<object> list,
            Predicate<Type> predicate,
            IEnumerable<object> filteredProperties)
        {
            foreach (var element in filteredProperties)
            {
                list.Add(element);

                foreach (var child in GetFlatternChildren(element.Extend(), list, predicate))
                {
                    list.Add(child);
                }
            }
        }
    }
}