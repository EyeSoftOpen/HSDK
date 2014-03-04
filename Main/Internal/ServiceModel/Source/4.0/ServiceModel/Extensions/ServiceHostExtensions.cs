namespace EyeSoft.ServiceModel
{
	using System.ServiceModel;
	using System.ServiceModel.Description;

	public static class ServiceHostExtensions
	{
		public static void AddBehavior(this ServiceHost serviceHost, IServiceBehavior behavior)
		{
			serviceHost.Description.Behaviors.Remove(behavior.GetType());
			serviceHost.Description.Behaviors.Add(behavior);
		}
	}
}