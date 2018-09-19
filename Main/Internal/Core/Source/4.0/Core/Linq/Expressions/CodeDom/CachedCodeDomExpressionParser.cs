namespace EyeSoft.Linq.Expressions.CodeDom
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	public class CachedCodeDomExpressionParser
		: CodeDomExpressionParser
	{
		private readonly IDictionary<string, object> expressionDictionary =
			new Dictionary<string, object>();

		public override Expression<Func<T, bool>> Parse<T>(string evalExpression)
		{
			if (!expressionDictionary.ContainsKey(evalExpression))
			{
				expressionDictionary.Add(evalExpression, base.Parse<T>(evalExpression));
			}

			return (Expression<Func<T, bool>>)expressionDictionary[evalExpression];
		}
	}
}