namespace EyeSoft.ServiceModel
{
	using System;
	using System.Net;
	using System.ServiceModel;
	using System.ServiceModel.Channels;

	using EyeSoft.Diagnostic;

	public class ChannelFactoryHelper<TServiceContract> where TServiceContract : class
	{
		private readonly EndpointAddress address;

		private readonly Binding binding;

		private readonly NetworkCredential credential;

		public ChannelFactoryHelper(
				string host = ServiceDefaults.Http.Host,
				int port = ServiceDefaults.Http.Port,
				NetworkCredential credential = null,
				string dnsName = null,
				Binding binding = null,
				string serviceName = null)
		{
			this.credential = credential;

			var url = new UriBuilder(host) { Port = port }.Uri.ToString();

			serviceName = serviceName ?? GetServiceFromContract();

			var uri = new Uri($"{url}{serviceName}");

			if (dnsName == null)
			{
				address = new EndpointAddress(uri);
			}
			else
			{
				var identity = EndpointIdentity.CreateDnsIdentity(dnsName);
				address = new EndpointAddress(uri, identity);
			}

			if (binding != null)
			{
				this.binding = binding;
			}
			else
			{
				this.binding = credential == null ? Bindings.Basic : Bindings.WsMessageWithUsername;
			}
		}

		public TResult Get<TResult>(Func<TServiceContract, TResult> func)
		{
			using (var factory = Create())
			{
				return factory.Get(func);
			}
		}

		public void Execute(Action<TServiceContract> action)
		{
			using (var factory = Create())
			{
				factory.Execute(action);
			}
		}

		public ChannelFactory<TServiceContract> Create()
		{
			if (SystemInspector.Debugger.IsAttached)
			{
				binding.SendTimeout = TimeSpan.FromHours(1);
				binding.ReceiveTimeout = binding.SendTimeout;
				binding.OpenTimeout = binding.SendTimeout;
				binding.CloseTimeout = binding.SendTimeout;
			}

			var factory = new ChannelFactory<TServiceContract>(binding, address);

			if (credential != null)
			{
				factory.Credentials.SetCredential(credential.UserName, credential.Password, true);
			}

			return factory;
		}

		private string GetServiceFromContract()
		{
			var type = typeof(TServiceContract);

			return type.IsInterface ? type.Name.Substring(1) : type.Name;
		}
	}
}