namespace EyeSoft.Linq.Expressions.Parsing
{
	public class ExpressionBuilder
	{
		private readonly ExpressionInfo expressionInfo;
		private int stepForwards;

		public ExpressionBuilder(ExpressionInfo expressionInfo)
		{
			this.expressionInfo = expressionInfo;
		}

		private int Token
		{
			get;
			set;
		}

		private string CurrentTokenText
		{
			get
			{
				return expressionInfo.Tokens[Token + stepForwards];
			}
		}

		public ExpressionResult Parse(int token)
		{
			var expressionToken = ExpressionToken.Create();

			stepForwards = 0;
			Token = token;

			while (!IsLastToken(token, stepForwards) && !ExpressionParserHelper.IsLogicToken(CurrentTokenText))
			{
				var expressionTokenText = expressionInfo.Tokens[token + stepForwards];
				expressionToken.Update(expressionInfo, expressionTokenText);

				stepForwards++;
			}

			return
				ExpressionResult.Create(expressionToken.Expression, stepForwards);
		}

		private bool IsLastToken(int token, int stepForwards)
		{
			return
				token + stepForwards >= expressionInfo.Tokens.Count;
		}
	}
}