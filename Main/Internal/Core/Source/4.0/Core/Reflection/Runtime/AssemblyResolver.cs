namespace EyeSoft.Reflection
{
	using System.Globalization;
	using System.Reflection;

	public class AssemblyResolver : IAssemblyResolver
	{
		private const string ResourceNameSeparator = ".";

		private readonly string resource;

		private readonly Assembly assemblyLoader;

		public AssemblyResolver(string resource = null, Assembly assemblyLoader = null)
		{
			this.assemblyLoader = assemblyLoader ?? Assembly.GetEntryAssembly();
			this.resource = resource ?? string.Concat(this.assemblyLoader.GetName().Name, ".References.");

			if (!this.resource.EndsWith(ResourceNameSeparator))
			{
				this.resource = string.Concat(this.resource, ResourceNameSeparator);
			}
		}

		public Assembly Resolve(AssemblyName name)
		{
			var resourcePath = Path(name);
			return ResolveAssembly(resourcePath);
		}

		public virtual bool CanResolve(AssemblyName assemblyName)
		{
			return true;
		}

		protected virtual string Path(AssemblyName name)
		{
			var assemblyName = name;

			var rootResourcePath = assemblyName.Name;

			var isInvariant = name.CultureInfo == null || name.CultureInfo.Equals(CultureInfo.InvariantCulture);

			if (!isInvariant)
			{
				var cultureFolderName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(assemblyName.CultureInfo.TwoLetterISOLanguageName);
				rootResourcePath = string.Concat(rootResourcePath, ResourceNameSeparator, cultureFolderName);
			}

			var path = string.Concat(resource, rootResourcePath, ".dll");

			return path;
		}

		protected virtual Assembly ResolveAssembly(string path)
		{
			using (var stream = assemblyLoader.GetManifestResourceStream(path))
			{
				if (stream == null)
				{
					return null;
				}

				var assemblyRawBytes = new byte[stream.Length];
				stream.Read(assemblyRawBytes, 0, assemblyRawBytes.Length);
				var resolvedAssembly = Assembly.Load(assemblyRawBytes);

				return resolvedAssembly;
			}
		}
	}
}