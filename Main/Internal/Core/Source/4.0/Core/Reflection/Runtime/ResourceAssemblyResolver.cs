namespace EyeSoft.Reflection
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using System.Security;

	using EyeSoft.Collections.Concurrent;

	[SecurityCritical]
	internal class ResourceAssemblyResolver : IAssembliesResolver
	{
		private static readonly object lockInstance = new object();

		private readonly TypeOnceCollection<IAssemblyResolver> assemblyDictionary =
			new TypeOnceCollection<IAssemblyResolver>();

		private readonly IDictionary<string, Assembly> loadedAssemblies = new Dictionary<string, Assembly>();

		public Assembly OnAssemblyResolve(ResolveEventArgs args)
		{
			lock (lockInstance)
			{
				var assemblyFullName = args.Name;

				if (assemblyFullName.StartsWith("mscorlib.resources", StringComparison.InvariantCultureIgnoreCase))
				{
					return null;
				}

				if (loadedAssemblies.ContainsKey(assemblyFullName))
				{
					return loadedAssemblies[assemblyFullName];
				}
				
				var assemblyName = new AssemblyName(assemblyFullName);

				var assemblyResolver = assemblyDictionary.SingleOrDefault(resolver => resolver.CanResolve(assemblyName));

				var assemblyResolved = assemblyResolver != null ? assemblyResolver.Resolve(assemblyName) : null;

				if (assemblyResolved != null)
				{
					loadedAssemblies.Add(assemblyFullName, assemblyResolved);
				}

				return assemblyResolved;
			}
		}

		IAssembliesResolver IAssembliesResolver.AppendResolver(IAssemblyResolver assemblyResolver)
		{
			assemblyDictionary.Add(assemblyResolver);
			return this;
		}
	}
}