namespace EyeSoft.ServiceModel
{
	using System.Security.Principal;
	using System.ServiceModel;

	internal class ServiceOperationContext : IOperationContext
	{
		public ServiceOperationContext()
		{
			ServiceSecurityContext = new InternalServiceSecurityContext();
		}

		public IServiceSecurityContext ServiceSecurityContext { get; private set; }

		private class InternalServiceSecurityContext : IServiceSecurityContext
		{
			public IIdentity PrimaryIdentity
			{
				get { return OperationContext.Current.ServiceSecurityContext.PrimaryIdentity; }
			}
		}
	}
}