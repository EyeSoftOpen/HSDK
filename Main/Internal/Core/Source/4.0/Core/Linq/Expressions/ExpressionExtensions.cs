namespace EyeSoft.Linq.Expressions
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;


	public static class ExpressionExtensions
	{
		public static Expression<Func<object, object>> ToObjectConstantLambda(this KeyValuePair<string, object> expectedValue)
		{
			return
				expectedValue
					.Value
					.ToObjectConstantLambda(expectedValue.Key);
		}

		public static Expression<Func<object, object>> ToObjectConstantLambda<T>(this T value, string parameterName = "x")
		{
			return ToConstantLambda<object>(value, parameterName);
		}

		public static Expression<Func<T, object>> ToConstantLambda<T>(this T value, string parameterName = "x")
		{
			var param = Expression.Parameter(typeof(object), parameterName);

			Expression expressionValue = Expression.Constant(value);

			if (value.GetType() != typeof(T))
			{
				expressionValue = Expression.Convert(expressionValue, typeof(T));
			}

			var lambda = Expression.Lambda<Func<T, object>>(expressionValue, param);

			return lambda;
		}

		public static IParameter<T> Parameter<T>(this Expression<Func<T>> expression)
		{
			return new Parameter<T>(expression);
		}
	}
}