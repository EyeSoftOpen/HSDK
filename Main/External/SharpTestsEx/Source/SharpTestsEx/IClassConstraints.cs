using System;
namespace SharpTestsEx
{
	/// <summary>
	/// Constraint over object instances.
	/// </summary>
	public interface IClassConstraints : IConstraints<object>, IBeConstraints<IClassBeConstraints>,
																			 INegableConstraints<IClassConstraints>, IAllowClone
	{
	}

	/// <summary>
	/// Constraints for object instance "Should Be"
	/// </summary>
	public interface IClassBeConstraints : IChildAndChainableConstraints<object, IClassConstraints>
	{
		/// <summary>
		/// Verifies that actual is equal to <paramref name="expected"/>.
		/// </summary>
		/// <param name="expected">The expected instance</param>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IClassConstraints> EqualTo(object expected);

		/// <summary>
		/// Verifies that the <see cref="object"/> is null.
		/// </summary>
		/// <returns>Chainable And constraint</returns>
		IAndConstraints<IClassConstraints> Null();

		/// <summary>
		/// Verifies that the actual is an instance of a specific type.
		/// </summary>
		/// <typeparam name="T">The expected <see cref="Type"/>.</typeparam>
		/// <returns>
		/// A <see cref="IClassConstraints{T}"/> for the instance converted to 
		/// the specified type to start a chained assertion.
		/// </returns>
		IAndConstraints<IClassConstraints<T>> OfType<T>();
	}

	/// <summary>
	/// Constraints for object instance of a specific gine <see cref="Type"/>.
	/// </summary>
	/// <typeparam name="TValue">The <see cref="Type"/> of the instance.</typeparam>
	public interface IClassConstraints<TValue> : IClassConstraints
	{
		/// <summary>
		/// The actual value
		/// </summary>
		TValue ValueOf { get; }

		/// <summary>
		/// The actual value
		/// </summary>
		TValue Value { get; }
	}
}