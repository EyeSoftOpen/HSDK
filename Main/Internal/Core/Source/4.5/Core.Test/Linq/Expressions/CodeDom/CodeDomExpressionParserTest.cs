namespace EyeSoft.Test.Linq.Expressions.CodeDom
{
	using EyeSoft.Linq.Expressions.CodeDom;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class CodeDomExpressionParserTest
	{
		[TestMethod]
		public void VerifySinglePropertyExpressionParsingUsingCodeDom()
		{
			new CodeDomExpressionParser()
				.Parse<TestEntity>(KnownExpressions.SingleProperty.Compiled)
				.ToString()
				.Should()
				.Be
				.EqualTo(KnownExpressions.SingleProperty.Compiled);
		}
	}
}