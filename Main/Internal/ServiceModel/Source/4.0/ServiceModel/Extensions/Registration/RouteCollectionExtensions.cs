namespace EyeSoft.ServiceModel.Registration
{
	using System.Collections.Generic;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.Web.Routing;

	using EyeSoft;
	using EyeSoft.ServiceModel;

	public static class RouteCollectionExtensions
	{
		public static RouteCollection AddService<TContract, TService>(
			this RouteCollection routes,
			Binding[] bindings = null,
			string[] allowedSchemes = null,
			ILocator locator = null,
			params IServiceBehavior[] behaviors)
			where TService : class, TContract
		{
			if (locator == null)
			{
				locator = Locator.Instance;
			}

			var serviceType = typeof(TService);

			var hostConfigurator = new ServiceHostConfigurator(typeof(TContract), behaviors, bindings);

			var serviceHostFactory = new LocatorServiceHostFactory(hostConfigurator, locator, allowedSchemes);

			var serviceRoute = new ServiceRoute(serviceType.Name, serviceHostFactory, serviceType);

			routes.Add(serviceRoute);

			return routes;
		}

		public static IServiceConfiguration ConfigureService<TContract, TService>(
			this ICollection<RouteBase> routes,
			string[] allowedSchemes = null,
			ILocator locator = null)
			where TService : class, TContract
		{
			if (locator == null)
			{
				locator = Locator.Instance;
			}

			return new ServiceConfiguration(locator, routes, allowedSchemes, typeof(TContract), typeof(TService));
		}
	}
}