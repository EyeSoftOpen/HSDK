namespace EyeSoft.Linq.Expressions.Parsing
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Linq.Expressions;

	public class ExpressionParser
		: IExpressionParser
	{
		private const int TokenToSkip = 2;

		public Expression<Func<T, bool>> Parse<T>(string evalExpression)
		{
			var expressionInfo =
				ExpressionInfo.Create<T>(evalExpression);

			var expressionBuilder =
				new ExpressionBuilder(expressionInfo);

			var expressionTokens = new List<Expression>();
			var logicOperatorTokens = new List<string>();

			var token = TokenToSkip;

			while (!EndOfTokens(expressionInfo, token))
			{
				var expressionResult =
					expressionBuilder.Parse(token);

				token += expressionResult.TokensParsed;

				expressionTokens.Add(expressionResult.Expression);

				if (EndOfTokens(expressionInfo, token))
				{
					break;
				}

				var expressionText = expressionInfo.Tokens[token];

				if (ExpressionParserHelper.IsLogicToken(expressionText))
				{
					logicOperatorTokens.Add(expressionText);
				}

				token++;
			}

			var logicOperatorIndex = 0;

			Expression expression = null;

			for (var index = 0; index < expressionTokens.Count; index++)
			{
				if (index == 0)
				{
					continue;
				}

				var logicFunc =
					ExpressionParserHelper
						.GetLogicExpression(logicOperatorTokens[logicOperatorIndex]);

				expression =
					logicFunc(expressionTokens[index - 1], expressionTokens[index]);

				logicOperatorIndex++;
			}

			if (expression == null)
			{
				expression = expressionTokens.Single();
			}

			return
				Expression.Lambda<Func<T, bool>>(expression, expressionInfo.Parameter);
		}

		private static bool EndOfTokens(ExpressionInfo expressionInfo, int token)
		{
			return
				token >= expressionInfo.Tokens.Count() - TokenToSkip;
		}
	}
}