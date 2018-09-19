namespace EyeSoft.Test.Linq.Expressions
{
	using System.Diagnostics;
	using System.Linq;

	using EyeSoft.Linq.Expressions;
	using EyeSoft.Linq.Expressions.CodeDom;
	using EyeSoft.Linq.Expressions.Parsing;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class ExpressionParserPerfomanceTest
	{
		[TestMethod]
		public void StringAndCodeDomExpressionParserPerformanceCheck()
		{
			var stringExpressionParser = new ExpressionParser();

			var codeDomExpressionParser = new CachedCodeDomExpressionParser();

			CheckPerformance(stringExpressionParser, KnownExpressions.SingleProperty.Source);

			CheckPerformance(codeDomExpressionParser, KnownExpressions.SingleProperty.Compiled);
		}

		private static void CheckPerformance(IExpressionParser expressionParser, string expression)
		{
			var stopwatch = Stopwatch.StartNew();

			Enumerable
				.Range(1, 100)
				.ToList()
				.ForEach(i => expressionParser.Parse<TestEntity>(expression));

			stopwatch.Stop();
			Debug.WriteLine("{0}: {1}", expressionParser.GetType().Name, stopwatch.ElapsedMilliseconds);
		}
	}
}