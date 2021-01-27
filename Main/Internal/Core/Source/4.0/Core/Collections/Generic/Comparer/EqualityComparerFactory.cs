namespace EyeSoft.Core.Collections.Generic
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Reflection;

    public static class EqualityComparerFactory<T>
	{
		public static IEqualityComparer<T> Create(Expression<Func<T, object>> expression, Func<T, int> hashCode = null)
		{
			return Create((object)expression, hashCode);
		}

		public static IEqualityComparer<T> Create(MemberExpression memberExpression, Func<T, int> hashCode = null)
		{
			return Create((object)memberExpression, hashCode);
		}

		public static IEqualityComparer<T> Create(object expression, Func<T, int> hashCode = null)
		{
			PropertyInfo propertyInfo;

			if (expression is Expression<Func<T, IComparable>>)
			{
				var propertyExpression = (Expression<Func<T, IComparable>>)expression;
				propertyInfo = propertyExpression.Property();
			}
			else
			{
				var memberExpression = expression as MemberExpression;
				propertyInfo = Reflector.Property(memberExpression);
			}

			return Create(propertyInfo, hashCode);
		}

		public static IEqualityComparer<T> Create(PropertyInfo propertyInfo, Func<T, int> hashCode = null)
		{
			Func<T, T, bool> localComparer = (x, y) =>
				{
					var value = propertyInfo.GetValue(x, null);
					var other = propertyInfo.GetValue(y, null);

					return value.Equals(other);
				};

			Func<T, int> localHashCode = x =>
				{
					var value = propertyInfo.GetValue(x, null);

					return value.GetHashCode();
				};


			return Create(localComparer, hashCode ?? localHashCode);
		}

		public static IEqualityComparer<T> Create(Func<T, T, bool> func, Func<T, int> hashCode)
		{
			return new FuncToEqualityComparer<T>(func, hashCode);
		}
	}
}