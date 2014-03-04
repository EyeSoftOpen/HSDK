namespace EyeSoft.ServiceLocator
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Mvc;

	using Microsoft.Practices.ServiceLocation;

	public abstract class ResolverContainer : ServiceLocator, IResolverContainer
	{
		protected ResolverContainer(bool throwExceptionOnMissingComponent = true)
			: base(throwExceptionOnMissingComponent)
		{
		}

		object IDependencyResolver.GetService(Type serviceType)
		{
			return !IsRegistered(serviceType) ? null : GetInstance(serviceType);
		}

		IEnumerable<object> IDependencyResolver.GetServices(Type serviceType)
		{
			return !IsRegistered(serviceType) ? Enumerable.Empty<object>() : GetAllInstances(serviceType);
		}

		TService IServiceLocator.GetInstance<TService>()
		{
			return Resolve<TService>();
		}

		TService IServiceLocator.GetInstance<TService>(string key)
		{
			return Resolve<TService>(key);
		}

		object IServiceLocator.GetInstance(Type serviceType)
		{
			return Resolve(serviceType);
		}

		object IServiceLocator.GetInstance(Type serviceType, string key)
		{
			return Resolve(serviceType, key);
		}

		IEnumerable<TService> IServiceLocator.GetAllInstances<TService>()
		{
			return ResolveAll<TService>();
		}

		IEnumerable<object> IServiceLocator.GetAllInstances(Type serviceType)
		{
			return ResolveAll(serviceType);
		}

		public abstract object Resolve(Type serviceType);

		public abstract object Resolve(Type serviceType, string key);

		public abstract IEnumerable<object> ResolveAll(Type serviceType);

		public abstract TService Resolve<TService>();

		public abstract TService Resolve<TService>(string key);

		public abstract IEnumerable<TService> ResolveAll<TService>();

		public abstract object Resolve(Type serviceType, IDictionary<string, object> parameters);

		public abstract T Resolve<T>(IDictionary<string, object> parameters);

		public abstract void Release(object obj);

		public abstract void Dispose();
	}
}