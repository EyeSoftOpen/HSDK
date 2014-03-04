namespace EyeSoft.Testing.ServiceModel
{
	using System;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;

	internal class EncapsulatedHost
	{
		private readonly ServiceHost host;

		private readonly ErrorHandlerBehavior errorHandlerBehavior;

		public EncapsulatedHost(
			ServiceHost host,
			Type serviceContractType,
			object service,
			Binding binding,
			string url,
			bool registerErrorHandler)
		{
			this.host = host;
			host.AddServiceEndpoint(serviceContractType, binding, url);

			RegisterServiceDebugBehavior(host);

			RegisterDependencyServiceBehavior(service, host);

			if (!registerErrorHandler)
			{
				return;
			}

			errorHandlerBehavior = new ErrorHandlerBehavior();
			RegisterErrorHandlerBehavior(errorHandlerBehavior, host);
		}

		public bool IsFaultedSupported
		{
			get
			{
				return errorHandlerBehavior != null;
			}
		}

		public bool IsFaulted
		{
			get
			{
				if (errorHandlerBehavior == null)
				{
					const string Message =
						"The IsFaulted property can be read only if an error hadler is registered. Check the IsFaultedSupported before.";

					new NotSupportedException(Message).Throw();
				}
				return errorHandlerBehavior.Faulted;
			}
		}

		public ServiceHost Host
		{
			get
			{
				return host;
			}
		}

		private void RegisterDependencyServiceBehavior(object serviceInstance, ServiceHostBase host)
		{
			host.Description.Behaviors.Add(new DependencyServiceBehavior(serviceInstance));
		}

		private void RegisterServiceDebugBehavior(ServiceHostBase host)
		{
			var serviceDebugBehavior = (ServiceDebugBehavior)host.Description.Behaviors[typeof(ServiceDebugBehavior)];
			serviceDebugBehavior.IncludeExceptionDetailInFaults = true;
		}

		private void RegisterErrorHandlerBehavior(ErrorHandlerBehavior errorHandlerBehavior, ServiceHostBase host)
		{
			if (host.Description.Behaviors.Find<ErrorHandlerBehavior>() == null)
			{
				host.Description.Behaviors.Add(errorHandlerBehavior);
			}
		}
	}
}