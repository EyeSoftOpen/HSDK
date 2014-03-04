namespace EyeSoft.Testing.ServiceModel
{
	using System;

	public class UnhandledErrorFault
	{
		public UnhandledErrorFault(Exception ex, string message, string stackTrace)
		{
			if (ex != null)
			{
				ExceptionType = ex.GetType().FullName;
			}

			Message = message;
			StackTrace = stackTrace;
		}

		public string ExceptionType
		{
			get;
			private set;
		}

		public string Message
		{
			get;
			private set;
		}

		public string StackTrace
		{
			get;
			private set;
		}
	}
}