namespace EyeSoft.Reflection
{
	using System;

	public static class AssembliesResolver
	{
		private static readonly IAssembliesResolver resourceAssemblyResolver;

		static AssembliesResolver()
		{
			var resolver = new ResourceAssemblyResolver();
			resourceAssemblyResolver = resolver;

			AppDomain.CurrentDomain.AssemblyResolve += (s, e) => resolver.OnAssemblyResolve(e);
		}

		public static IAssembliesResolver AppendDefaultResolver()
		{
			var assemblyResolver = new AssemblyResolver();
			return AppendResolver(assemblyResolver);
		}

		public static IAssembliesResolver AppendResolver(string resourceRootPath)
		{
			var resolver = new AssemblyResolver(resourceRootPath);
			return AppendResolver(resolver);
		}

		public static IAssembliesResolver AppendResolver(IAssemblyResolver assemblyResolver)
		{
			return resourceAssemblyResolver.AppendResolver(assemblyResolver);
		}
	}
}