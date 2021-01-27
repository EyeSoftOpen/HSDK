using System;
using System.Linq.Expressions;

namespace SharpTestsEx.Assertions
{
	public class ExpressionMethodCallAssertion<TA> : UnaryAssertion<TA>
	{
		private readonly ParameterExpression actualParameter;
		private readonly MethodCallExpression expression;
		private readonly Func<TA, bool> compiledMatcher;

		public ExpressionMethodCallAssertion(ParameterExpression actual, MethodCallExpression expression)
		{
			if (actual == null)
			{
				throw new ArgumentNullException("actual");
			}
			if (expression == null)
			{
				throw new ArgumentNullException("expression");
			}
			actualParameter = actual;
			this.expression = expression;
			compiledMatcher = Expression.Lambda<Func<TA, bool>>(expression, actualParameter).Compile();
		}

		public override Func<TA, bool> Matcher => compiledMatcher;

        private string MatcherString()
		{
			return new ExpressionStringBuilder(expression).ToString();
		}

		public override string GetMessage(TA actual, string customMessage)
		{
			return string.Format("{0} {1} {2}.{3}", Messages.FormatValue(actual), Properties.Resources.AssertionVerb, string.Format("Satisfy ({0})", MatcherString()), customMessage);
		}
	}
}