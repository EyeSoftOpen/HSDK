namespace EyeSoft.CodeDom.Converters
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	using EyeSoft.Text;

	internal class MethodConverter
		: IToStringConverter<Method>
	{
		private const int Tabulations = 2;
		private readonly IEnumerable<string> importCollection;

		public MethodConverter()
		{
			importCollection = Enumerable.Empty<string>();
		}

		public MethodConverter(IEnumerable<string> importCollection)
		{
			this.importCollection = importCollection;
		}

		public string ConvertToString(Method method)
		{
			var methodBuilder =
				new StringBuilder();

			methodBuilder
				.AppendIndentedFormat(
					Tabulations,
					"{0} {1} {2}",
					method.Visibility.ToString().ToLower(),
					method.ReturnType.FriendlyName(importCollection),
					method.Name);

			methodBuilder.OpenRoundBracket();

			var index = 0;

			var methodParameterConverter = new MethodParameterConverter();

			foreach (var parameter in method.ParameterList)
			{
				methodBuilder.Append(methodParameterConverter.ConvertToString(parameter));
				index++;

				if (index == method.ParameterList.Count())
				{
					break;
				}

				methodBuilder.AppendComma();
			}

			methodBuilder.CloseRoundBracket();

			methodBuilder.OpenBracket(Tabulations);

			var sourceLines =
				method
				.Source
				.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
				.Select(line => new string('\t', Tabulations + 1) + line);

			var tabulatedSource =
				string.Join(Environment.NewLine, sourceLines);

			methodBuilder.AppendLine(tabulatedSource);
			methodBuilder.CloseBracket(Tabulations);

			return methodBuilder.ToString();
		}
	}
}