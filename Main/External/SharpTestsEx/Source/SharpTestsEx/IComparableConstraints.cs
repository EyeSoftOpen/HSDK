using System;

namespace SharpTestsEx
{
	/// <summary>
	/// Constraint over <see cref="IComparable"/> instances.
	/// </summary>
	/// <typeparam name="T">The concrete type of actual value.</typeparam>
	public interface IComparableConstraints<T> : IBeConstraints<IComparableBeConstraints<T>>,
																							 INegableConstraints<IComparableConstraints<T>>, IConstraints<T>,
																							 IAllowClone
	{
	}

	/// <summary>
	/// Constraints for <see cref="IComparable"/> instance ("Should Be")
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IComparableBeConstraints<T> : IConstraints<T>
	{
		/// <summary>
		/// Verifies that actual is equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> EqualTo(IComparable expected);

		/// <summary>
		/// Verifies that actual is greater than <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> GreaterThan(IComparable expected);

		/// <summary>
		/// Verifies that actual is less than <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> LessThan(IComparable expected);

		/// <summary>
		/// Verifies that actual is greater than or equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> GreaterThanOrEqualTo(IComparable expected);

		/// <summary>
		/// Verifies that actual is less than or equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> LessThanOrEqualTo(IComparable expected);

		/// <summary>
		/// Verifies that actual is included in the range <paramref name="lowLimit"/>-<paramref name="highLimit"/>.
		/// </summary>
		/// <param name="lowLimit">The less aceptable value.</param>
		/// <param name="highLimit">The higher aceptable value.</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IComparableConstraints<T>> IncludedIn(IComparable lowLimit, IComparable highLimit);
	}
}