using System;

namespace SharpTestsEx
{
	public static class ComparableConstraintsExtensions
	{
		public static IAndConstraints<IComparableConstraints<T>> Be<T>(this IComparableConstraints<T> constraint, IComparable expected)
		{
			return constraint.Be.EqualTo(expected);
		}

		public static IAndConstraints<IComparableConstraints<T>> Be<T>(this INullableComparableConstraints<T> constraint, IComparable expected)
		{
			return constraint.Be.EqualTo(expected);
		}
	}
}