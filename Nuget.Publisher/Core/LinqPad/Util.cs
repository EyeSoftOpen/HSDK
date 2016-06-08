namespace EyeSoft.Nuget.Publisher.Core.LinqPad
{
	using System;

	using EyeSoft.Nuget.Publisher.Core.Core;

	public static class Util
	{
		public static string ReadLine(string prompt)
		{
			ConsoleHelper.WriteLine(prompt);
			return Console.ReadLine();
		}
	}
}