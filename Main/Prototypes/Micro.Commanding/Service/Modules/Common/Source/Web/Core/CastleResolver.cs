namespace EyeSoft.Accounting.Prototype.Api.Web.Core
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Web.Http.Dependencies;

	using Castle.Windsor;

	internal class CastleResolver : IDependencyResolver
	{
		private readonly IWindsorContainer container;

		public CastleResolver(IWindsorContainer container)
		{
			if (container == null)
			{
				throw new ArgumentNullException(nameof(container));
			}

			this.container = container;
		}

		public object GetService(Type serviceType)
		{
			return container.Kernel.HasComponent(serviceType) ? container.Resolve(serviceType) : null;
		}

		public IEnumerable<object> GetServices(Type serviceType)
		{
			return container.ResolveAll(serviceType).Cast<object>();
		}

		public IDependencyScope BeginScope()
		{
			return new ReleasingDependencyScope(this, container.Release);
		}

		public void Dispose()
		{
			container.Dispose();
		}
	}
}