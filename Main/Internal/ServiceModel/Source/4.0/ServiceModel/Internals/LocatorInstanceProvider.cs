namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	using EyeSoft;

	internal class LocatorInstanceProvider : IInstanceProvider, IContractBehavior
	{
		private readonly ILocator locator;

		private readonly Type serviceType;

		public LocatorInstanceProvider(ILocator locator, Type serviceType)
		{
			this.locator = locator;
			this.serviceType = serviceType;
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return GetInstance(instanceContext);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return locator.Resolve(serviceType);
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			locator.Release(instance);
		}

		public void AddBindingParameters(
			ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
		{
		}

		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		public void ApplyDispatchBehavior(
			ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
		{
			dispatchRuntime.InstanceProvider = this;
		}

		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}
	}
}