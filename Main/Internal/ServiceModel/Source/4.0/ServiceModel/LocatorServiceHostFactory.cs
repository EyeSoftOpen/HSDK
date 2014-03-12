namespace EyeSoft.ServiceModel
{
	using System;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Activation;

	using EyeSoft;

	public class LocatorServiceHostFactory : ServiceHostFactory
	{
		private readonly ServiceHostConfigurator configurator;

		private readonly ILocator locator;

		private readonly string[] allowedSchemes;

		public LocatorServiceHostFactory(
			ServiceHostConfigurator configurator,
			ILocator locator,
			string[] allowedSchemes)
		{
			this.configurator = configurator;
			this.locator = locator;
			this.allowedSchemes = allowedSchemes;
		}

		protected override ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
		{
			var serviceHost = new LocatorServiceHost(locator, configurator.ImplementedContract, serviceType, baseAddresses);

			var addresses = baseAddresses;

			if (allowedSchemes != null)
			{
				addresses = baseAddresses.Where(x => allowedSchemes.Contains(x.Scheme)).ToArray();
			}

			configurator.Configure(serviceHost, addresses);

			return serviceHost;
		}
	}
}