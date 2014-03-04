namespace EyeSoft.Test.Linq.Expressions.Parsing
{
	using EyeSoft.Linq.Expressions.Parsing;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ExpressionParserTest
	{
		[TestMethod]
		public void VerifySinglePropertyExpression()
		{
			VerifyPropertyExpression(KnownExpressions.SingleProperty);
		}

		[TestMethod]
		public void VerifyMorePropertyExpression()
		{
			VerifyPropertyExpression(KnownExpressions.MultipleProperty);
		}

		private void VerifyPropertyExpression(ExpressionConstant expressionConstant)
		{
			new ExpressionParser()
				.Parse<TestEntity>(expressionConstant.Source)
				.ToString()
				.Should()
				.Be
				.EqualTo(expressionConstant.Compiled);
		}
	}
}