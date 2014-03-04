namespace EyeSoft.Test.Helpers
{
	using System;

	internal static class KnownCode
	{
		public static readonly string VoidMethod =
			"namespace EyeSoft.Sample" + Environment.NewLine +
			"{" + Environment.NewLine +
			"	using System;" + Environment.NewLine +
			string.Empty + Environment.NewLine +
			"	public class Test" + Environment.NewLine +
			"	{" + Environment.NewLine +
			"		public void Print(string line)" + Environment.NewLine +
			"		{" + Environment.NewLine +
			"			Console.WriteLine(line);" + Environment.NewLine +
			"		}" + Environment.NewLine +
			"	}" + Environment.NewLine +
			"}";

		public static readonly string ReturnTypeMethod =
			"namespace EyeSoft.Sample" + Environment.NewLine +
			"{" + Environment.NewLine +
			"	using System;" + Environment.NewLine +
			string.Empty + Environment.NewLine +
			"	public class Test" + Environment.NewLine +
			"	{" + Environment.NewLine +
			"		public string Read(string line)" + Environment.NewLine +
			"		{" + Environment.NewLine +
			"			Console.WriteLine(line);" + Environment.NewLine +
			"			return Console.ReadLine();" + Environment.NewLine +
			"		}" + Environment.NewLine +
			"	}" + Environment.NewLine +
			"}";
	}
}