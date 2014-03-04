namespace EyeSoft.Test.Helpers
{
	using System;
	using System.Linq.Expressions;

	internal class ExpressionConstant
	{
		public ExpressionConstant(string source, Expression<Func<TestEntity, bool>> compiled)
		{
			Source = source;
			Compiled = compiled.ToString();
		}

		public string Source { get; private set; }

		public string Compiled { get; private set; }
	}
}