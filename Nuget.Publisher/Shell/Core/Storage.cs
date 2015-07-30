namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.LinqPad;

	public static class Storage
	{
		private static readonly bool canWrite;

		static Storage()
		{
			var choice = Util.ReadLine("Overwrite all Nuspec and AssemblyInfo files for real? [Y, N]");
			canWrite = choice.Equals("y", StringComparison.InvariantCultureIgnoreCase);
		}

		public static string[] ReadAllLines(string path)
		{
			return File.ReadAllLines(path);
		}

		public static string ReadAllText(string path)
		{
			return File.ReadAllText(path);
		}

		public static void WriteAllText(string path, string contents)
		{
			if (!canWrite)
			{
				////Console.WriteLine("Simulated write of contents in {0} file.", path);
				return;
			}

			File.WriteAllText(path, contents);
		}

		public static void WriteAllLines(string path, string[] contents)
		{
			if (!canWrite)
			{
				////Console.WriteLine("Simulated write of contents in {0} file.", path);
				return;
			}

			File.WriteAllLines(path, contents);
		}
	}
}