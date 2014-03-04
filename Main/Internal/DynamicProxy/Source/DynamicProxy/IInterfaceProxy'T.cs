namespace EyeSoft.DynamicProxy
{
	using Castle.DynamicProxy;

	public interface IInterfaceProxy<T>
	{
		IInterfaceProxy<T> Implements<TInterface>();

		IInterceptorProxy<T> Interceptor<TInterceptor>()
			where TInterceptor : class, IInterceptor, new();
	}
}