namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Description;

	using EyeSoft;

	internal class LocatorServiceHost : ServiceHost
	{
		private readonly Type contractType;

		public LocatorServiceHost(ILocator locator, Type contractType, Type serviceType, params Uri[] baseAddresses)
			: base(serviceType, baseAddresses)
		{
			this.contractType = contractType;

			foreach (var contractDescription in ImplementedContracts.Values)
			{
				contractDescription.Behaviors.Add(ContractBehavior(locator, serviceType));
			}
		}

		private IContractBehavior ContractBehavior(ILocator locator, Type serviceType)
		{
			var provider =
				locator.IsRegistered(contractType) ?
					(IContractBehavior)new LocatorInstanceProvider(locator, contractType) :
					new ActivatorInstanceProvider(serviceType);

			return provider;
		}
	}
}