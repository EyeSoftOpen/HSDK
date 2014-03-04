namespace EyeSoft.Testing.ServiceModel
{
	using System;
	using System.ServiceModel.Channels;

	public class ServiceConfiguration
	{
		public ServiceConfiguration(
			Binding binding,
			string hostName,
			int port,
			long maxReceivedMessageSize,
			int maxBufferSize,
			bool prepareForLongOperation)
		{
			Binding = binding;
			HostName = hostName;
			Port = port;
			MaxReceivedMessageSize = maxReceivedMessageSize;
			MaxBufferSize = maxBufferSize;
			PrepareForLongOperation = prepareForLongOperation;

			Url =
				string
					.Format(
						"{0}://{1}:{2}//{3}",
						binding.Scheme,
						hostName,
						port,
						Guid.NewGuid());
		}

		public Binding Binding { get; private set; }

		public string HostName { get; private set; }

		public int Port { get; private set; }

		public long MaxReceivedMessageSize { get; private set; }

		public int MaxBufferSize { get; private set; }

		public bool PrepareForLongOperation { get; private set; }

		public string Url { get; private set; }
	}
}