using System;

namespace SharpTestsEx.Assertions
{
	public class OrAssertion<TA> : BitwiseAssertion<TA>
	{
		public OrAssertion(UnaryAssertion<TA> left, UnaryAssertion<TA> right)
			: base("Or", left, right) {}

		public override Func<TA, bool> Matcher
		{
			get
			{
				return a => Left.Matcher(a) || Right.Matcher(a);
			}
		}
	}
}