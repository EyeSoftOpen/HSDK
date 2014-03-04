namespace EyeSoft.ServiceModel.Registration
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel.Activation;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.Web.Routing;

	using EyeSoft;
	using EyeSoft.ServiceModel;

	internal class ServiceConfiguration : IServiceConfiguration, IServiceConfigurationBehaviors
	{
		private readonly ILocator locator;

		private readonly ICollection<RouteBase> routes;

		private readonly Type contractType;

		private readonly Type serviceType;

		private IServiceBehavior[] serviceBehaviors;

		public ServiceConfiguration(ILocator locator, ICollection<RouteBase> routes, Type contractType, Type serviceType)
		{
			this.locator = locator;
			this.routes = routes;
			this.contractType = contractType;
			this.serviceType = serviceType;
		}

		public IServiceConfigurationBehaviors Behaviors(params IServiceBehavior[] behaviors)
		{
			serviceBehaviors = behaviors;

			return this;
		}

		public ICollection<RouteBase> Bindings(params Binding[] bindings)
		{
			var hostConfigurator = new ServiceHostConfigurator(contractType, serviceBehaviors, bindings);

			routes.Add(new ServiceRoute(serviceType.Name, new LocatorServiceHostFactory(hostConfigurator, locator), serviceType));

			return routes;
		}
	}
}