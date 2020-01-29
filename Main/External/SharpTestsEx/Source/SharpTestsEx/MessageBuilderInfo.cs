namespace SharpTestsEx
{
	/// <summary>
	/// Collection information to build the failure message
	/// </summary>
	/// <typeparam name="TActual">Type of the actual value.</typeparam>
	/// <typeparam name="TExpected">Type of the expected value.</typeparam>
	public class MessageBuilderInfo<TActual, TExpected>
	{
		/// <summary>
		/// The actual value under test.
		/// </summary>
		public TActual Actual { get; set; }

		/// <summary>
		/// The expected value of the test.
		/// </summary>
		public TExpected Expected { get; set; }

		/// <summary>
		/// The name of the assertion
		/// </summary>
		/// <example>
		/// "be EqualTo"
		/// </example>
		public string AssertionPredicate { get; set; }

		/// <summary>
		/// The user custom message.
		/// </summary>
		public string CustomMessage { get; set; }
	}
}