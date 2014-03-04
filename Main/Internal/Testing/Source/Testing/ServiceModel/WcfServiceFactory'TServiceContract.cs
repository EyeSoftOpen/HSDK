namespace EyeSoft.Testing.ServiceModel
{
	using System;
	using System.Diagnostics;
	using System.ServiceModel;
	using System.Xml;

	internal class WcfServiceFactory<TServiceContract>
	{
		private TimeSpan timeSpan = new TimeSpan(0, 0, 20);

		public EncapsulatedHost Create(TServiceContract service, ServiceConfiguration serviceConfiguration, bool registerErrorHandler)
		{
			PrepareForLongOperation(serviceConfiguration);

			serviceConfiguration.Binding.OpenTimeout = timeSpan;
			serviceConfiguration.Binding.CloseTimeout = timeSpan;
			serviceConfiguration.Binding.SendTimeout = timeSpan;
			serviceConfiguration.Binding.ReceiveTimeout = timeSpan;

			var host =
				new ServiceHost(service.GetType())
					{
						CloseTimeout = timeSpan,
						OpenTimeout = timeSpan
					};

			var encapsulatedHost =
				new EncapsulatedHost(
					host,
					typeof(TServiceContract),
					service,
					serviceConfiguration.Binding,
					serviceConfiguration.Url,
					registerErrorHandler);

			try
			{
				host.Open();
			}
			catch (TimeoutException timeoutException)
			{
				Debug.WriteLine("Failed to close the service host - Exception: " + timeoutException.Message);
				throw;
			}
			catch (CommunicationException communicationException)
			{
				if (communicationException.Message.Contains("10013"))
				{
					PortAlreadyUsed(serviceConfiguration, communicationException);
				}

				Debug.WriteLine("Failed to close the service host - Exception: " + communicationException.Message);
				throw;
			}

			return encapsulatedHost;
		}

		private void PrepareForLongOperation(ServiceConfiguration serviceConfiguration)
		{
			if (!serviceConfiguration.PrepareForLongOperation)
			{
				return;
			}

			timeSpan = new TimeSpan(0, 0, 50, 0);

			IncreaseReaderQuotas(serviceConfiguration);

			IncreaseAllowedMessage(serviceConfiguration);
		}

		private void IncreaseAllowedMessage(ServiceConfiguration serviceConfiguration)
		{
			var dynamicBinding = (dynamic)serviceConfiguration.Binding;
			dynamicBinding.MaxBufferPoolSize = int.MaxValue;
			dynamicBinding.MaxBufferSize = serviceConfiguration.MaxBufferSize;
			dynamicBinding.MaxReceivedMessageSize = serviceConfiguration.MaxReceivedMessageSize;
		}

		private void IncreaseReaderQuotas(ServiceConfiguration serviceConfiguration)
		{
			const int SaneButUsableLimit = 1000;

			var readerQuotas = new XmlDictionaryReaderQuotas
				{
					MaxArrayLength = 100 * 1024,
					////Set max array size to 100kb
					MaxStringContentLength = SaneButUsableLimit,
					MaxBytesPerRead = SaneButUsableLimit,
					MaxDepth = SaneButUsableLimit,
					MaxNameTableCharCount = SaneButUsableLimit
				};

			serviceConfiguration.Binding.GetType().GetProperty("ReaderQuotas").SetValue(
				serviceConfiguration.Binding, readerQuotas, null);
		}

		private void PortAlreadyUsed(ServiceConfiguration serviceConfiguration, CommunicationException innerException)
		{
			var message =
				string.Format("The port {0} used for the test could be in use by another application.", serviceConfiguration.Port);

			throw new CommunicationException(message, innerException);
		}
	}
}