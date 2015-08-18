namespace EyeSoft.ServiceModel.Hosting.Web
{
	using System;
	using System.Collections.Generic;
	using System.Web.Http.Dependencies;

	internal class DependencyResolver : IDependencyResolver
	{
		private readonly ILocator locator;

		public DependencyResolver(ILocator locator)
		{
			this.locator = locator;
		}

		public object GetService(Type serviceType)
		{
			return locator.Resolve(serviceType);
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return locator.ResolveAll(serviceType);
		}

		public IDependencyScope BeginScope()
		{
			throw new NotImplementedException();
		}

		public void Dispose()
		{
			locator.Dispose();
		}
	}
}