namespace EyeSoft.Linq.Expressions.Parsing
{
	using System;
	using System.ComponentModel;
	using System.Linq.Expressions;
	using System.Reflection;

	internal class ExpressionToken
	{
		private ExpressionToken()
		{
		}

		public MemberExpression PropertyExpression { get; private set; }

		public PropertyInfo PropertyInfo { get; private set; }

		public Func<MemberExpression, ConstantExpression, BinaryExpression> OperationExpression { get; private set; }

		public Expression Expression { get; set; }

		public static ExpressionToken Create()
		{
			return new ExpressionToken();
		}

		public void Update(
			ExpressionInfo expressionInfo,
			string expressionTokenText)
		{
			if (ExpressionParserHelper.IsPropertyToken(expressionInfo.ParameterSyntax, expressionTokenText))
			{
				SetPropertyExpression(expressionInfo, expressionTokenText);

				return;
			}

			if (OperationExpression != null)
			{
				var constantValue = expressionTokenText;

				if (PropertyInfo.PropertyType == typeof(string))
				{
					constantValue = constantValue.Replace("'", null);
				}

				var comparisonConstantValue =
					TypeDescriptor
						.GetConverter(PropertyInfo.PropertyType)
						.ConvertFromString(constantValue);

				Expression =
					OperationExpression(PropertyExpression, Expression.Constant(comparisonConstantValue));

				return;
			}

			if (ExpressionParserHelper.GetOperatorFunc(expressionTokenText) != null)
			{
				OperationExpression = ExpressionParserHelper.GetOperatorFunc(expressionTokenText);
			}
		}

		private void SetPropertyExpression(ExpressionInfo expressionInfo, string expressionTokenText)
		{
			PropertyExpression = ExpressionParserHelper.GetPropertyExpression(
				expressionInfo.Parameter, expressionInfo.ParameterSyntax, expressionTokenText);

			PropertyInfo = (PropertyInfo)this.PropertyExpression.Member;

			if (PropertyInfo.PropertyType == typeof(bool))
			{
				Expression = Expression.IsTrue(this.PropertyExpression);
			}
		}
	}
}