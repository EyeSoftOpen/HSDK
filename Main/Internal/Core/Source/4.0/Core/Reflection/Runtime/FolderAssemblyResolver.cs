namespace EyeSoft.Core.Reflection
{
    using System.IO;
    using System.Reflection;
    using IO;

    public class FolderAssemblyResolver : IAssemblyResolver
	{
		private readonly string assembliesFolder;

		public FolderAssemblyResolver(string assembliesFolder)
		{
			this.assembliesFolder = assembliesFolder;
		}

		public Assembly Resolve(AssemblyName name)
		{
			var assemblyPath = AssemblyPath(name);

			var loadedAssembly = Assembly.LoadFrom(assemblyPath);

			return loadedAssembly;
		}

		public bool CanResolve(AssemblyName assemblyName)
		{
			return Storage.File(AssemblyPath(assemblyName)).Exists;
		}

		private string AssemblyPath(AssemblyName name)
		{
			return Path.Combine(assembliesFolder, string.Concat(name.Name, ".dll"));
		}
	}
}