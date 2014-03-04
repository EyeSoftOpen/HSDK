namespace EyeSoft
{
	using System;
	using System.Diagnostics;
	using System.Reflection;
	using System.Text;

	public static class ExceptionExtensions
	{
		public static void Throw(this Exception exception)
		{
			exception.MantainOriginalStackTrace();
			throw exception;
		}

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

		[DebuggerStepThrough]
		internal static void MantainOriginalStackTrace(this Exception exception)
		{
			const BindingFlags BindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod;

			typeof(Exception).InvokeMember("PrepForRemoting", BindingFlags, null, exception, new object[0]);
		}
	}
}