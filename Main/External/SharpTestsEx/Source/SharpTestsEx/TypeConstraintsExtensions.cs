using System;
using System.Reflection;
using SharpTestsEx.Assertions;
using SharpTestsEx.ExtensionsImpl;

namespace SharpTestsEx
{
	public static class TypeConstraintsExtensions
	{
		public static IAndConstraints<ITypeConstraints> AssignableTo(this ITypeBeConstraints constraint, Type expected)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<Type, Type>(Properties.Resources.PredicateBeAssignableTo, expected, expected.GetTypeInfo().IsAssignableFrom));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<ITypeConstraints> AssignableTo<T>(this ITypeBeConstraints constraint)
		{
			return AssignableTo(constraint, typeof(T));
		}

		public static IAndConstraints<ITypeConstraints> Be(this ITypeConstraints constraint, Type expected)
		{
			return constraint.Be.EqualTo(expected);
		}

		public static IAndConstraints<ITypeConstraints> Be<T>(this ITypeConstraints constraint)
		{
			return constraint.Be.EqualTo<T>();
		}
	}
}