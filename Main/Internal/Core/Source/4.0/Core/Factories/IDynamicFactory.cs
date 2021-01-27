namespace EyeSoft.Core
{
	public interface IDynamicFactory
	{
		T Create<T>();
	}
}