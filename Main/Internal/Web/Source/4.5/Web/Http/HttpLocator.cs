namespace EyeSoft.Web.Http
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Dependencies;

	using EyeSoft;

	public class HttpLocator : IDependencyResolver
	{
		private readonly ILocator locator;

		public HttpLocator(ILocator locator)
		{
			this.locator = locator;
		}

		public object GetService(Type serviceType)
		{
			return locator.IsRegistered(serviceType) ? locator.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return new List<object>();
		}

		public IDependencyScope BeginScope()
		{
			return this;
		}

		public void Dispose()
		{
			locator.Dispose();
		}
	}
}