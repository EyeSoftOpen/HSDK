namespace EyeSoft.Core.Reflection
{
    using System.Reflection;

    public interface IAssemblyResolver
	{
		Assembly Resolve(AssemblyName name);

		bool CanResolve(AssemblyName assemblyName);
	}
}