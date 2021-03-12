namespace EyeSoft.Core.Test.Reflection
{
    using System;
    using System.Linq.Expressions;
    using EyeSoft.Linq.Expressions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

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
				.Should()
				.Be("x", "The variable name of the expression is wrong.");

			parameter
				.Value
				.Should()
				.Be(Test, "The value of the expression is wrong.");
		}
	}
}