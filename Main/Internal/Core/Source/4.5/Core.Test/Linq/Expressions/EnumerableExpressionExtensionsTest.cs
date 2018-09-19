namespace EyeSoft.Test.Linq.Expressions
{
	using System.Collections.Generic;

	using EyeSoft.Linq.Expressions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EnumerableExpressionExtensions
	{
		[TestMethod]
		public void VerifyExpressionsToDictionary()
		{
			var expectedValue1 = KeyValue("name", "Bill");
			var expectedValue2 = KeyValue("age", 30);

			new[]
				{
					expectedValue1.ToObjectConstantLambda(),
					expectedValue2.ToObjectConstantLambda()
				}
			.ToDictionary()
			.Should().Have.SameSequenceAs(
				new[]
					{
						expectedValue1,
						expectedValue2
					});
		}

		private static KeyValuePair<string, object> KeyValue<T>(string parameterName, T value)
		{
			return new KeyValuePair<string, object>(parameterName, value);
		}
	}
}