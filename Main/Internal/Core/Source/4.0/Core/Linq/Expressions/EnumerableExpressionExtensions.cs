namespace EyeSoft.Linq.Expressions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public static class EnumerableExpressionExtensions
	{
		public static IDictionary<string, object> ToDictionary(
			this IEnumerable<Expression<Func<object, object>>> parameters)
		{
			return
				parameters
					.ToDictionary(
						parameter =>
						parameter.Parameters.Single().Name,
						parameter =>
						parameter.Compile()(null));
		}
	}
}