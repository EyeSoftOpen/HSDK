namespace SharpTestsEx.Assertions
{
	/// <summary>
	/// Verifies that the specified object is null. The assertion fails if it is not null.
	/// </summary>
	/// <typeparam name="TA">Type of the actual value subject of the assertion.</typeparam>
	public class NullAssertion<TA> : Assertion<TA, object> where TA : class
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="NullAssertion{TA}"/> class.
		/// </summary>
		public NullAssertion() : base(Properties.Resources.PredicateBeNull, null, a => ReferenceEquals(null, a), mi=>
			string.Format("{0} {1} {2}.{3}", Messages.FormatValue(mi.Actual), Properties.Resources.AssertionVerb, mi.AssertionPredicate,mi.CustomMessage)) { }
	}
}