namespace EyeSoft.ServiceModel
{
	using System;
	using System.Collections.Generic;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;

	public class ServiceHostConfigurator
	{
		private readonly IServiceBehavior[] behaviors;

		private readonly Binding[] bindings;

		public ServiceHostConfigurator(Type implementedContract, IServiceBehavior[] behaviors, Binding[] bindings)
		{
			ImplementedContract = implementedContract;
			this.behaviors = behaviors;
			this.bindings = bindings;
		}

		public Type ImplementedContract { get; private set; }

		public void Configure(ServiceHost serviceHost, IEnumerable<Uri> baseAddresses)
		{
			foreach (var behavior in behaviors)
			{
				serviceHost.AddBehavior(behavior);
			}

			foreach (var uri in baseAddresses)
			{
				foreach (var binding in bindings)
				{
					serviceHost.AddServiceEndpoint(ImplementedContract, binding, uri);
				}
			}
		}
	}
}