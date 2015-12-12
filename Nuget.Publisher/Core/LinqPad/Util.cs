namespace EyeSoft.Nuget.Publisher.Shell.LinqPad
{
	using System;

	public static class Util
	{
		public static string ReadLine(string prompt)
		{
			ConsoleHelper.WriteLine(prompt);
			return Console.ReadLine();
		}
	}
}