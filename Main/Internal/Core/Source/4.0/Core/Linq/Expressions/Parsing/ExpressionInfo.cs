namespace EyeSoft.Linq.Expressions.Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq.Expressions;
    using Collections.Generic;

    public class ExpressionInfo
	{
		private ExpressionInfo(IEnumerable<string> tokens, Type type, string[] expressionTokens, string parameterName)
		{
			Tokens = tokens.AsReadOnly();
			ExpressionTokens = expressionTokens;
			ParameterName = parameterName;
			ParameterSyntax = ParameterName + ".";
			Parameter = Expression.Parameter(type, parameterName);
		}

		public ReadOnlyCollection<string> Tokens { get; }

		public string[] ExpressionTokens { get; }

		public string ParameterName { get; }

		public ParameterExpression Parameter { get; }

		public string ParameterSyntax { get; }

		public static ExpressionInfo Create<T>(string expression)
		{
			var expressionTokens =
				expression.Split(' ');

			if (expressionTokens[1] != "=>")
			{
				throw new ArgumentException("Cannot find the \"goes to\" operator.");
			}

			var parameterName = expressionTokens[0];

			return
				new ExpressionInfo(
					expressionTokens,
					typeof(T),
					expressionTokens,
					parameterName);
		}
	}
}