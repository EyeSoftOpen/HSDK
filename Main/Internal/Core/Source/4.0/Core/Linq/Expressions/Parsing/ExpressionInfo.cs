namespace EyeSoft.Linq.Expressions.Parsing
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq.Expressions;

	using EyeSoft.Collections.Generic;

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

		public ReadOnlyCollection<string> Tokens { get; private set; }

		public string[] ExpressionTokens { get; private set; }

		public string ParameterName { get; private set; }

		public ParameterExpression Parameter { get; private set; }

		public string ParameterSyntax { get; private set; }

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