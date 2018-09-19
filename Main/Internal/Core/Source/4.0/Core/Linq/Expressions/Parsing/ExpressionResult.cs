namespace EyeSoft.Linq.Expressions.Parsing
{
	using System.Linq.Expressions;

	public class ExpressionResult
	{
		private ExpressionResult(Expression expression, int tokensParsed)
		{
			Expression = expression;
			TokensParsed = tokensParsed;
		}

		public Expression Expression { get; private set; }

		public int TokensParsed { get; private set; }

		public static ExpressionResult Create(Expression expression, int stepsForward)
		{
			return
				new ExpressionResult(expression, stepsForward);
		}
	}
}