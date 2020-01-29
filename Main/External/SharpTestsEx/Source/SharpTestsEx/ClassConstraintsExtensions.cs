using System;
using SharpTestsEx.Assertions;
using SharpTestsEx.ExtensionsImpl;
using System.Reflection;

namespace SharpTestsEx
{
	/// <summary>
	/// Extensions for constraint over object instances.
	/// </summary>
	public static class ClassConstraintsExtensions
	{
		/// <summary>
		/// Verifies that actual is the same instance than <paramref name="expected"/>.
		/// </summary>
		/// <param name="constraint">The <see cref="IClassBeConstraints"/> extented.</param>
		/// <param name="expected">The expected object instance.</param>
		/// <returns>Chainable And constraint</returns>
		public static IAndConstraints<IClassConstraints> SameInstanceAs(this IClassBeConstraints constraint, object expected)
		{
			constraint.AssertionInfo.AssertUsing(new SameInstanceAssertion<object, object>(expected));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		/// <summary>
		/// Verifies that actual is an instance of <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> expected.</typeparam>
		/// <param name="constraint">The <see cref="IClassBeConstraints"/> extented.</param>
		/// <returns>Chainable And constraint</returns>
		public static IAndConstraints<IClassConstraints<T>> InstanceOf<T>(this IClassBeConstraints constraint)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<object, object>(Properties.Resources.PredicateBeInstanceOf,
			                                                                   typeof (T),
			                                                                   a => a != null && typeof (T).GetTypeInfo().IsInstanceOfType(a)));
			return
				new AndConstraint<IClassConstraints<T>>(new ClassConstraints<T>(constraint.AssertionInfo.Actual,
																																				() => constraint.AssertionInfo.FailureMessage));
		}

		/// <summary>
		/// Verifies that actual instance is assignable from <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> expected.</typeparam>
		/// <param name="constraint">The <see cref="IClassBeConstraints"/> extented.</param>
		/// <returns>Chainable And constraint</returns>
		public static IAndConstraints<IClassConstraints> AssignableFrom<T>(this IClassBeConstraints constraint)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<object, object>(Properties.Resources.PredicateBeAssignableFrom,
			                                                                   typeof (T),
			                                                                   a =>
			                                                                   a != null
			                                                                   && a.GetType().GetTypeInfo().IsAssignableFrom(typeof (T))));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		/// <summary>
		/// Verifies that actual instance is assignable to <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The <see cref="Type"/> expected.</typeparam>
		/// <param name="constraint">The <see cref="IClassBeConstraints"/> extented.</param>
		/// <returns>Chainable And constraint</returns>
		public static IAndConstraints<IClassConstraints> AssignableTo<T>(this IClassBeConstraints constraint)
		{
			constraint.AssertionInfo.AssertUsing(new Assertion<object, object>(Properties.Resources.PredicateBeAssignableTo,
			                                                                   typeof (T),
			                                                                   a =>
			                                                                   a != null
			                                                                   && typeof (T).GetTypeInfo().IsAssignableFrom(a.GetType())));
			return ConstraintsHelper.AndChain(constraint.AssertionParent);
		}

		public static IAndConstraints<IClassConstraints> Be(this IClassConstraints constraint, object expected)
		{
			return constraint.Be.EqualTo(expected);
		}
	}
}