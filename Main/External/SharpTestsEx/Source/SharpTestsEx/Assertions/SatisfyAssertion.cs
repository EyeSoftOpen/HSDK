using System;
using System.Linq.Expressions;

namespace SharpTestsEx.Assertions
{
	public class SatisfyAssertion<TA> : IAssertion<TA>
	{
		private readonly Expression<Func<TA, bool>> assertionExpression;

		public SatisfyAssertion(Expression<Func<TA, bool>> expression)
		{
			assertionExpression = expression;
		}

		#region Implementation of IAssertion<TA>

		public void Assert(TA actual, string customMessage)
		{
			var ev = new ExpressionVisitor<TA>(actual, assertionExpression);
			var assertion = ev.Visit();
			assertion.Assert(actual, customMessage);
		}

		#endregion
	}
}