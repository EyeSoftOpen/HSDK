namespace SharpTestsEx
{
	/// <summary>
	/// Constraint over boolean values.
	/// </summary>
	public interface IBooleanConstraints : IBeConstraints<IBooleanBeConstraints>
	{
	}

	/// <summary>
	/// Constraints for boolean "Should Be"
	/// </summary>
	public interface IBooleanBeConstraints : IConstraints<bool>
	{
		/// <summary>
		/// Verifies that actual is true.
		/// </summary>
		void True();

		/// <summary>
		/// Verifies that actual is false.
		/// </summary>
		void False();

		void EqualTo(bool expected);
	}
}