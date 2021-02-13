namespace EyeSoft.IO
{
    using System.IO;
    using System.Reflection;

    public static class FileSystem
	{
		public static string GetCurrentDirectory()
		{
			return Assembly.GetEntryAssembly().GetCurrentDirectory();
		}

		public static string GetCurrentDirectory(this Assembly assembly)
		{
			return Path.GetDirectoryName(assembly.Location);
		}

		public static string GetCurrentDirectoryWithoutDebug()
		{
			var currentDirectory = new DirectoryInfo(GetCurrentDirectory());

			if (currentDirectory.Name == "Debug")
			{
				currentDirectory = currentDirectory.Parent.Parent;
			}

			return currentDirectory.FullName;
		}
	}
}