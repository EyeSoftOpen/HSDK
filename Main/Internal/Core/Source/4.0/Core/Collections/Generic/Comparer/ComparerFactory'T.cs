namespace EyeSoft.Collections.Generic
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using Reflection;

    public static class ComparerFactory<T>
	{
		public static IComparer<T> Create(Expression<Func<T, IComparable>> expression)
		{
			var propertyExpression = expression;
			var propertyInfo = propertyExpression.Property();

			return Create(propertyInfo);
		}

		public static IComparer<T> Create(MemberExpression memberExpression)
		{
			var propertyInfo = Reflector.Property(memberExpression);

			return Create(propertyInfo);
		}

		public static IComparer<T> Create(PropertyInfo propertyInfo)
		{
			Func<T, T, int> localComparer = (x, y) =>
				{
					var value = (IComparable)propertyInfo.GetValue(x, null);
					var other = (IComparable)propertyInfo.GetValue(y, null);

					return Comparer.Default.Compare(value, other);
				};

			return Create(localComparer);
		}

		public static IComparer<T> Create(Func<T, T, int> func)
		{
			return new FuncToComparer<T>(func);
		}

		public static IComparer Create(IComparer<T> comparer)
		{
			return new FuncToComparer((x, y) => comparer.Compare((T)x, (T)y));
		}
	}
}