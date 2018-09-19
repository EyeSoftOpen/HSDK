namespace EyeSoft.Linq.Expressions.Parsing
{
	using System;
	using System.Collections.Generic;
	using System.Linq.Expressions;

	internal static class ExpressionParserHelper
	{
		private static readonly IDictionary<string, Func<Expression, Expression, BinaryExpression>> logicOperatorDictionary =
			new Dictionary<string, Func<Expression, Expression, BinaryExpression>>
				{
					{ "&", Expression.AndAlso },
					{ "|", Expression.OrElse }
				};

		private static readonly IDictionary<string, Func<MemberExpression, ConstantExpression, BinaryExpression>> operatorDictionary =
			new Dictionary<string, Func<MemberExpression, ConstantExpression, BinaryExpression>>
				{
					{ "=", Expression.Equal },
					{ ">", Expression.GreaterThan },
					{ "<", Expression.LessThan }
				};

		public static Func<Expression, Expression, BinaryExpression> GetLogicExpression(string logicOperator)
		{
			return
				logicOperatorDictionary[logicOperator];
		}

		public static Func<MemberExpression, ConstantExpression, BinaryExpression> GetOperatorFunc(string expressionOperator)
		{
			return
				operatorDictionary[expressionOperator];
		}

		public static bool IsPropertyToken(string parameterSyntax, string expressionToken)
		{
			return
				expressionToken
					.StartsWith(parameterSyntax);
		}

		public static MemberExpression GetPropertyExpression(
			ParameterExpression parameterExpression,
			string parameterSyntax,
			string expressionToken)
		{
			var propertyName = expressionToken.Replace(parameterSyntax, null);
			var propertyExpression = Expression.Property(parameterExpression, propertyName);
			return propertyExpression;
		}

		public static bool IsLogicToken(string expressionToken)
		{
			return
				logicOperatorDictionary
					.ContainsKey(expressionToken);
		}
	}
}