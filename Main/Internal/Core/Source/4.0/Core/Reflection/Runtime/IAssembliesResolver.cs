namespace EyeSoft.Core.Reflection
{
	public interface IAssembliesResolver
	{
		IAssembliesResolver AppendResolver(IAssemblyResolver assemblyResolver);
	}
}