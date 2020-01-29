namespace SharpTestsEx.Assertions
{
	/// <summary>
	/// Verifies that two specified instances are the same object instance..
	/// </summary>
	/// <typeparam name="TA">Type of the actual value.</typeparam>
	/// <typeparam name="TE">Type of the expected value.</typeparam>
	public class SameInstanceAssertion<TA, TE> : Assertion<TA, TE>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="SameInstanceAssertion{TA,TE}"/> class.
		/// </summary>
		/// <param name="expected">The value to compare.</param>
		public SameInstanceAssertion(TE expected)
			: base(Properties.Resources.PredicateBeSameAs, expected, a => ReferenceEquals(a, expected)) {}
	}
}