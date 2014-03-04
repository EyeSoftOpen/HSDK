namespace EyeSoft.Reflection
{
	public interface IAssembliesResolver
	{
		IAssembliesResolver AppendResolver(IAssemblyResolver assemblyResolver);
	}
}