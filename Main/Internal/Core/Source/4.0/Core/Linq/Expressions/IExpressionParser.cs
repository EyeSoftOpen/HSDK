namespace EyeSoft.Linq.Expressions
{
	using System;
	using System.Linq.Expressions;

	public interface IExpressionParser
	{
		Expression<Func<T, bool>> Parse<T>(string evalExpression);
	}
}