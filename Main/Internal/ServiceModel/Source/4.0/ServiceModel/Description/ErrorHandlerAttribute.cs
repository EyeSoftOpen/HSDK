namespace EyeSoft.ServiceModel.Description
{
	using System;
	using System.Collections.ObjectModel;
	using System.ServiceModel;
	using System.ServiceModel.Channels;
	using System.ServiceModel.Description;
	using System.ServiceModel.Dispatcher;
	using System.Xml;

	[AttributeUsage(AttributeTargets.Class)]
	public abstract class ErrorHandlerAttribute : Attribute, IErrorHandler, IServiceBehavior
	{
		public virtual void ProvideFault(Exception error, MessageVersion version, ref Message fault)
		{
			if (fault != null)
			{
				return;
			}

			var faultException = new FaultException(error.Message);
			var messageFault = faultException.CreateMessageFault();
			fault = Message.CreateMessage(version, messageFault, "Error");
		}

		public virtual bool HandleError(Exception exception)
		{
			var context = OperationContext.Current;

			if (context == null)
			{
				return true;
			}

			ProcessUnhandledException(exception);

			return true;
		}

		public virtual void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
		}

		public virtual void AddBindingParameters(
			ServiceDescription serviceDescription,
			ServiceHostBase serviceHostBase,
			Collection<ServiceEndpoint> endpoints,
			BindingParameterCollection bindingParameters)
		{
		}

		public virtual void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
		{
			foreach (ChannelDispatcher chanDisp in serviceHostBase.ChannelDispatchers)
			{
				chanDisp.ErrorHandlers.Add(this);
			}
		}

		protected abstract void ProcessUnhandledException(Exception exception);
	}
}