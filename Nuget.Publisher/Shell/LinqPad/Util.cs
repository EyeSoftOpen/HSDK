namespace EyeSoft.Nuget.Publisher.Shell.LinqPad
{
	using System;

	public static class Util
	{
		public static string ReadLine(string prompt)
		{
			Console.WriteLine(prompt);
			return Console.ReadLine();
		}
	}
}