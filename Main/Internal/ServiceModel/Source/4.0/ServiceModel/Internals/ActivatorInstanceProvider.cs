namespace EyeSoft.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	internal class ActivatorInstanceProvider : IInstanceProvider, IContractBehavior
	{
		private readonly Type serviceType;

		public ActivatorInstanceProvider(Type serviceType)
		{
			this.serviceType = serviceType;
		}

		public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
		{
		}

		public void ApplyDispatchBehavior(
			ContractDescription contractDescription,
			ServiceEndpoint endpoint,
			DispatchRuntime dispatchRuntime)
		{
		}

		public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
		{
		}

		public void AddBindingParameters(
			ContractDescription contractDescription,
			ServiceEndpoint endpoint,
			BindingParameterCollection bindingParameters)
		{
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return GetInstance(instanceContext);
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			var instance = Activator.CreateInstance(serviceType);
			return instance;
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
			var disposable = instance as IDisposable;

			if (disposable == null)
			{
				return;
			}

			disposable.Dispose();
		}
	}
}