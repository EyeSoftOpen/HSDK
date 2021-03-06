namespace EyeSoft.Core.Test.Helpers
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

		public string Source { get; }

		public string Compiled { get; }
	}
}