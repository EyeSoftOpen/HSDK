namespace EyeSoft.Testing.ServiceModel
{
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	public class DependencyServiceBehavior
		: IServiceBehavior
	{
		private readonly object serviceInstance;

		public DependencyServiceBehavior(object serviceInstance)
		{
			this.serviceInstance = serviceInstance;
		}

		public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (var endpoint in serviceHostBase.ChannelDispatchers.OfType<ChannelDispatcher>().SelectMany(cd => cd.Endpoints))
			{
				endpoint.DispatchRuntime.InstanceProvider =
					new InstanceProvider(serviceInstance);
			}
		}

		public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public void AddBindingParameters(
			ServiceDescription serviceDescription,
			ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection bindingParameters)
		{
		}
	}
}