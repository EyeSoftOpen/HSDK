using SharpTestsEx.Properties;

namespace SharpTestsEx.Assertions
{
	/// <summary>
	/// Verifies that two specified generic type data are equal. The assertion fails if they are not equal.
	/// </summary>
	/// <typeparam name="TA">Type of the actual value.</typeparam>
	/// <typeparam name="TE">Type of the expected value.</typeparam>
	/// <remarks>
	/// The comparison is done ny the base <see cref="object.Equals(object,object)"/>.
	/// </remarks>
	public class ObjectEqualsAssertion<TA, TE> : Assertion<TA, TE>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ObjectEqualsAssertion{TA,TE}"/> class.
		/// </summary>
		/// <param name="expected">The value to compare.</param>
		public ObjectEqualsAssertion(TE expected)
			: base(Resources.PredicateBeEqualTo, expected, a => Equals(a, expected), info =>
			                                                                         	{
			                                                                         		string typeDescriptionOfActual =
			                                                                         			ReferenceEquals(info.Actual,
			                                                                         			                null)
			                                                                         				? ""
			                                                                         				: string.Format("(of {0})",
			                                                                         				                info.Actual.
			                                                                         				                	GetType().
			                                                                         				                	FullName);
																																									string typeDescriptionOfExpected =
																																										ReferenceEquals(info.Expected,
																																																		null)
																																											? ""
																																											: string.Format("(of {0})",
																																																			info.Expected.
																																																				GetType().
																																																				FullName);
																																									if(typeDescriptionOfActual == typeDescriptionOfExpected)
																																									{
																																										typeDescriptionOfActual = typeDescriptionOfExpected = string.Empty;
																																									}
			                                                                         		return
			                                                                         			string.Format(
			                                                                         				"{0}{5} {1} {2} {3}{6}.{4}",
			                                                                         				Messages.FormatValue(
			                                                                         					info.Actual),
			                                                                         				Resources.
			                                                                         					AssertionVerb,
			                                                                         				info.AssertionPredicate,
			                                                                         				Messages.FormatValue(
			                                                                         					info.Expected),
			                                                                         				info.CustomMessage, typeDescriptionOfActual, typeDescriptionOfExpected);
			                                                                         	})
		{
		}
	}
}