namespace EyeSoft.Test.Reflection
{
	using System;
	using System.Linq.Expressions;

	using EyeSoft.Linq.Expressions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ExpressionExtensionsTest
	{
		[TestMethod]
		public void CreateParameterFromExpressionAndCheckNameAndValueAreCorrect()
		{
			const string Test = "test";

			var x = Test;

			Expression<Func<string>> expression = () => x;

			var parameter = expression.Parameter();

			parameter
				.Name
				.Should("The variable name of the expression is wrong.")
				.Be.EqualTo("x");

			parameter
				.Value
				.Should("The value of the expression is wrong.")
				.Be.EqualTo(Test);
		}
	}
}