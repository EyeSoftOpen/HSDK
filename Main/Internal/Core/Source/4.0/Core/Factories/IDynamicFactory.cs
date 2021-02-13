namespace EyeSoft
{
	public interface IDynamicFactory
	{
		T Create<T>();
	}
}