namespace EyeSoft.Testing.ServiceModel
{
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Dispatcher;

	public class InstanceProvider
		: IInstanceProvider
	{
		private readonly object serviceInstance;

		public InstanceProvider(object serviceInstance)
		{
			this.serviceInstance = serviceInstance;
		}

		public object GetInstance(InstanceContext instanceContext)
		{
			return GetInstance(instanceContext, null);
		}

		public object GetInstance(InstanceContext instanceContext, Message message)
		{
			return serviceInstance;
		}

		public void ReleaseInstance(InstanceContext instanceContext, object instance)
		{
		}
	}
}