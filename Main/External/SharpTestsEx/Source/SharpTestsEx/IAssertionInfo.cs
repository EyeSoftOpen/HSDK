namespace SharpTestsEx
{
	/// <summary>
	/// Assertion information.
	/// </summary>
	/// <typeparam name="T">The type of the value subject of the assertion.</typeparam>
	public interface IAssertionInfo<T>
	{
		/// <summary>
		/// Subject of the assertion.
		/// </summary>
		T Actual { get; }

		/// <summary>
		/// The assertion is negated ?
		/// </summary>
		bool IsNegated { get; }

		/// <summary>
		/// The title of the assertion ("message" in MsTests terminology)
		/// </summary>
		string FailureMessage { get; }
	}
}