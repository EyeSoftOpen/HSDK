namespace EyeSoft.Core.Logging
{
    using System;

    public class ConsoleLogger : ILogger
	{
		public void Write(string message)
		{
			Console.WriteLine(message);
		}

		public void Error(Exception exception)
		{
			Console.WriteLine(exception.Message);
		}
	}
}