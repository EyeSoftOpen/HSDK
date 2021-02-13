namespace EyeSoft
{
    using System;
    using System.Collections.Generic;

    public interface IResolverLocator : IDisposable
	{
		bool IsRegistered(Type serviceType);

		object Resolve(Type serviceType);

		object Resolve(Type serviceType, string key);

		IEnumerable<object> ResolveAll(Type serviceType);

		TService Resolve<TService>();

		TService Resolve<TService>(string key);

		IEnumerable<TService> ResolveAll<TService>();

		object Resolve(Type serviceType, IDictionary<string, object> parameters);

		T Resolve<T>(IDictionary<string, object> parameters);

		void Release(object obj);
	}
}