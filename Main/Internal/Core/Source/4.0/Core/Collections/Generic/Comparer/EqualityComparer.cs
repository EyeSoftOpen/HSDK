namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public static class EqualityComparer
	{
		private static readonly object lockInstance = new object();

		private static readonly IDictionary<Type, Func<object, object, bool>> comparers =
			new Dictionary<Type, Func<object, object, bool>>();

		public static bool AreEquals(object value, object other)
		{
			Type type;

			if ((value == null) && (other == null))
			{
				return true;
			}

			if (value == null)
			{
				type = other.GetType();
			}
			else if (other == null)
			{
				type = value.GetType();
			}
			else
			{
				if (value.GetType() != other.GetType())
				{
					throw new ArgumentException("Cannot compare objects of different type.");
				}

				type = value.GetType();
			}

			var comparer = GetComparer(type);

			return comparer(value, other);
		}

		private static Func<object, object, bool> GetComparer(Type type)
		{
			lock (lockInstance)
			{
				if (!comparers.ContainsKey(type))
				{
					var comparer =
						typeof(EqualityComparer<>)
							.MakeGenericType(type)
							.GetProperty("Default", BindingFlags.Static | BindingFlags.Public)
							.GetValue(null, null);

					var methodInfo =
						comparer
							.GetType()
							.GetMethods(BindingFlags.Instance | BindingFlags.Public)
							.Single(method => method.Name == "Equals" && method.GetParameters().Length == 2);

					Func<object, object, bool> equal = (x, y) => (bool)methodInfo.Invoke(comparer, new[] { x, y });

					comparers.Add(type, equal);
				}
			}

			var cachedEqual = comparers[type];

			return cachedEqual;
		}
	}
}