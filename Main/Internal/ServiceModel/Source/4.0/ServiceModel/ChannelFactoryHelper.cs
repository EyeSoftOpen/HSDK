namespace EyeSoft.ServiceModel
{
	using System;
	using System.Net;
	using System.ServiceModel;
	using System.ServiceModel.Channels;

	public class ChannelFactoryHelper
	{
		private readonly string host;

		private readonly int port;

		private readonly NetworkCredential credential;

		private readonly string dnsName;

		private readonly Binding binding;

		public ChannelFactoryHelper(
			string host = ServiceDefaults.Http.Host,
			int port = ServiceDefaults.Http.Port,
			NetworkCredential credential = null,
			string dnsName = null,
			Binding binding = null)
		{
			this.host = host;
			this.port = port;
			this.credential = credential;
			this.dnsName = dnsName;
			this.binding = binding;
		}

		public TResult Get<TServiceContract, TResult>(Func<TServiceContract, TResult> func, string serviceName = null) where TServiceContract : class
		{
			using (var factory = Create<TServiceContract>(serviceName))
			{
				return factory.Get(func);
			}
		}

		public void Execute<TServiceContract>(Action<TServiceContract> action, string serviceName = null) where TServiceContract : class
		{
			using (var factory = Create<TServiceContract>(serviceName))
			{
				factory.Execute(action);
			}
		}

		public ChannelFactory<TServiceContract> Create<TServiceContract>(string serviceName = null) where TServiceContract : class
		{
			return new ChannelFactoryHelper<TServiceContract>(host, port, credential, dnsName, binding, serviceName).Create();
		}
	}
}