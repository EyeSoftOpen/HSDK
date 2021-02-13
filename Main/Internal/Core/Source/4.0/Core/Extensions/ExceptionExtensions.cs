namespace EyeSoft.Extensions
{
    using System;
    using System.Text;

    public static class ExceptionExtensions
	{

		public static string Format(this Exception exception)
		{
			var stringBuilder = new StringBuilder();

			while (exception != null)
			{
				stringBuilder
					.AppendLine("Message")
					.AppendLine(exception.Message).AppendLine()
					.AppendLine("StackTrace")
					.AppendLine(exception.StackTrace).AppendLine();

				exception = exception.InnerException;
			}

			var result = stringBuilder.ToString().TrimEnd(Environment.NewLine.ToCharArray());

			return result;
		}

		public static string FormatXml(this Exception exception)
		{
			return new ExceptionXElement(exception).ToString();
		}
	}
}