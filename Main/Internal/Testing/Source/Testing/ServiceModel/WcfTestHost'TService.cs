namespace EyeSoft.Testing.ServiceModel
{
	using System;
	using System.Diagnostics;
	using System.ServiceModel;

	public class WcfTestHost<TServiceContract>
		: IDisposable
	{
		private readonly ServiceConfiguration serviceConfiguration;

		private readonly bool registerErrorHandler;

		private readonly TServiceContract serviceInstance;

		private EncapsulatedHost encapsulatedHost;
		private ChannelFactory<TServiceContract> factory;
		private TServiceContract proxy;

		public WcfTestHost(TServiceContract serviceInstance, ServiceConfiguration serviceConfiguration, bool registerErrorHandler)
		{
			this.serviceConfiguration = serviceConfiguration;
			this.registerErrorHandler = registerErrorHandler;
			this.serviceInstance = serviceInstance;
		}

		public TServiceContract CreateProxy()
		{
			encapsulatedHost =
				new WcfServiceFactory<TServiceContract>()
					.Create(serviceInstance, serviceConfiguration, registerErrorHandler);

			factory = new ChannelFactory<TServiceContract>(serviceConfiguration.Binding);
			proxy = factory.CreateChannel(new EndpointAddress(serviceConfiguration.Url));

			return proxy;
		}

		public void Dispose()
		{
			if (factory != null)
			{
				if (factory.State == CommunicationState.Opened)
				{
					if (encapsulatedHost.IsFaultedSupported)
					{
						if (!encapsulatedHost.IsFaulted)
						{
							factory.Close();
						}
					}
					else
					{
						try
						{
							factory.Close();
						}
						catch
						{
						}
					}
				}

				factory = null;
			}

			TearDown();
		}

		private void TearDown()
		{
			if (encapsulatedHost == null)
			{
				return;
			}

			try
			{
				if (encapsulatedHost.Host.State == CommunicationState.Opened || encapsulatedHost.Host.State == CommunicationState.Opening)
				{
					encapsulatedHost.Host.Close(new TimeSpan(0, 0, 0, 1));
				}
			}
			catch (TimeoutException timeoutEx)
			{
				Debug.WriteLine("Failed to close the service host - Exception: " + timeoutEx.Message);
			}
			catch (CommunicationException communicationEx)
			{
				Debug.WriteLine("Failed to close the service host - Exception: " + communicationEx.Message);
			}
			catch (Exception ex)
			{
				Debug.WriteLine("Exception: " + ex.Message);
				throw;
			}
			finally
			{
				encapsulatedHost = null;
			}
		}
	}
}