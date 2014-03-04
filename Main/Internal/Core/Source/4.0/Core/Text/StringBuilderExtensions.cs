namespace EyeSoft.Text
{
	using System.Text;

	public static class StringBuilderExtensions
	{
		public static StringBuilder AppendIndentedFormat(
			this StringBuilder stringBuilder,
			int tabulations,
			string format,
			params object[] args)
		{
			stringBuilder.Append(new string('\t', tabulations));
			stringBuilder.AppendFormat(format, args);
			return stringBuilder;
		}

		public static StringBuilder AppendLineFormat(this StringBuilder stringBuilder, string format, params object[] args)
		{
			stringBuilder.AppendFormat(format, args);
			stringBuilder.AppendLine();
			return stringBuilder;
		}

		public static StringBuilder AppendIndentedLineFormat(
			this StringBuilder stringBuilder,
			int tabulations,
			string format,
			params object[] args)
		{
			stringBuilder.AppendLineFormat(new string('\t', tabulations) + format, args);
			return stringBuilder;
		}
	}
}