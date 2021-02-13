namespace EyeSoft.Text
{
    using System.Text;

    public static class CodeStringBuilderExtensions
	{
		internal static StringBuilder OpenBracket(this StringBuilder stringBuilder, int tabulations = 0)
		{
			if (tabulations > 0)
			{
				stringBuilder.Append(new string('\t', tabulations));
			}

			stringBuilder.AppendLine("{");
			return stringBuilder;
		}

		internal static StringBuilder OpenRoundBracket(this StringBuilder stringBuilder)
		{
			stringBuilder.Append("(");
			return stringBuilder;
		}

		internal static StringBuilder CloseBracket(this StringBuilder stringBuilder, int tabulations = 0)
		{
			if (tabulations > 0)
			{
				stringBuilder.Append(new string('\t', tabulations));
			}

			stringBuilder.AppendLine("}");
			return stringBuilder;
		}

		internal static StringBuilder CloseRoundBracket(this StringBuilder stringBuilder)
		{
			stringBuilder.AppendLine(")");
			return stringBuilder;
		}

		internal static StringBuilder AppendComma(this StringBuilder stringBuilder)
		{
			stringBuilder.Append(",");
			return stringBuilder;
		}
	}
}