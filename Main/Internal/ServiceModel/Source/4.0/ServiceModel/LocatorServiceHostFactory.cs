namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Activation;

	using EyeSoft;

	public class LocatorServiceHostFactory : ServiceHostFactory
	{
		private readonly ServiceHostConfigurator configurator;

		private readonly ILocator locator;

		public LocatorServiceHostFactory(ServiceHostConfigurator configurator, ILocator locator)
		{
			this.configurator = configurator;
			this.locator = locator;
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			var serviceHost = new LocatorServiceHost(locator, configurator.ImplementedContract, serviceType, baseAddresses);

			configurator.Configure(serviceHost, baseAddresses);

			return serviceHost;
		}
	}
}