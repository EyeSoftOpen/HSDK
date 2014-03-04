namespace EyeSoft.Reflection
{
	using System.IO;
	using System.Reflection;

	using EyeSoft.IO;

	public static class AssemblyRuntime
	{
		private const string DebugFolder = "Debug";

		public static string GetCurrentWithoutDebug()
		{
			var currentDirectory = Storage.Directory(GetCurrent());

			if (currentDirectory.Name == DebugFolder)
			{
				currentDirectory = currentDirectory.Parent.Parent;
			}

			return currentDirectory.FullName;
		}

		public static string GetCurrent()
		{
			var entryAssembly = Assembly.GetEntryAssembly() ?? Assembly.GetCallingAssembly();

			return Path.GetDirectoryName(entryAssembly.Location);
		}
	}
}
