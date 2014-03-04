namespace EyeSoft.DynamicProxy
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using Castle.DynamicProxy;

	public class FluentProxy<T> :
		IInterfaceProxy<T>,
		IInterceptorProxy<T>
		where T : class
	{
		private readonly IList<Type> interfaceCollection = new List<Type>();
		private readonly IList<IInterceptor> interceptorCollection = new List<IInterceptor>();

		private readonly ProxyGenerator proxyGenenerator;

		public FluentProxy(ProxyBuilder proxyBuilder)
		{
			proxyGenenerator = proxyBuilder.ProxyGenerator;
		}

		public IInterfaceProxy<T> Implements<TInterface>()
		{
			interfaceCollection.Add(typeof(TInterface));
			return this;
		}

		IInterceptorProxy<T> IInterfaceProxy<T>.Interceptor<TInterceptor>()
		{
			interceptorCollection.Add(new TInterceptor());
			return this;
		}

		T IInterceptorProxy<T>.Create()
		{
			var proxy =
				proxyGenenerator
					.CreateClassProxy(
						typeof(T),
						interfaceCollection.ToArray(),
						ProxyGenerationOptions.Default,
						interceptorCollection.ToArray());

			return proxy as T;
		}
	}
}