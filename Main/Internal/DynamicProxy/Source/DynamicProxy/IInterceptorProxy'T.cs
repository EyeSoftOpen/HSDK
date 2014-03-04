namespace EyeSoft.DynamicProxy
{
	public interface IInterceptorProxy<T>
	{
		IInterfaceProxy<T> Implements<TInterface>();

		T Create();
	}
}