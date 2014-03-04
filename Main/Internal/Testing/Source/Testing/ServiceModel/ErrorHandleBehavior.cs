namespace EyeSoft.Testing.ServiceModel
{
	using System;
	using System.Collections.ObjectModel;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;

	internal class ErrorHandlerBehavior :
		IErrorHandler,
		IServiceBehavior
	{
		public bool Faulted
		{
			get;
			private set;
		}

		private Type ServiceType
		{
			get;
			set;
		}

		void IServiceBehavior.Validate(ServiceDescription description, ServiceHostBase host)
		{
		}

		void IServiceBehavior.AddBindingParameters(
			ServiceDescription description,
			ServiceHostBase host,
			Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection parameters)
		{
		}

		void IServiceBehavior.ApplyDispatchBehavior(
			ServiceDescription description,
			ServiceHostBase host)
		{
			ServiceType = description.ServiceType;

			foreach (ChannelDispatcher dispatcher in host.ChannelDispatchers)
			{
				dispatcher.ErrorHandlers.Add(this);
			}
		}

		bool IErrorHandler.HandleError(Exception error)
		{
			return false;
		}

		void IErrorHandler.ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			Faulted = true;

			var faultException = error as FaultException;

			if (faultException == null)
			{
				var data = new UnhandledErrorFault(error, error.Message, error.StackTrace);
				faultException = new FaultException<UnhandledErrorFault>(data);
			}

			var messageFault = faultException.CreateMessageFault();
			fault = Message.CreateMessage(version, messageFault, faultException.Action);
		}
	}
}