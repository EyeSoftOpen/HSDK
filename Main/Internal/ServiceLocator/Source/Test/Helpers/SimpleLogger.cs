namespace EyeSoft.ServiceLocator.Test.Helpers
{
	using System;

	public class SimpleLogger : ILogger
	{
		public void Log(string msg)
		{
			Console.WriteLine(msg);
		}
	}
}