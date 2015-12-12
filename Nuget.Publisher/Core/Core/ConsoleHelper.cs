namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;

	public static class ConsoleHelper
	{
		public static void WriteLine()
		{
			Execute(Console.WriteLine);
		}

		public static void WriteLine(string format)
		{
			Execute(() => Console.WriteLine(format));
		}

		public static void WriteLine(string format, params object[] arguments)
		{
			Execute(() => Console.WriteLine(format, arguments));
		}

		private static void Execute(Action action)
		{
			action();
		}
	}
}