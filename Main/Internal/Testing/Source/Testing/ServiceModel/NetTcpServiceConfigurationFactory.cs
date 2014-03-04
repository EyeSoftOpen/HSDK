namespace EyeSoft.Testing.ServiceModel
{
	using System.ServiceModel;

	public static class NetTcpServiceConfigurationFactory
	{
		private const string HostName = "localhost";

		private const int Port = 8500;

		public static ServiceConfiguration CreateForLongOperationWithMessageSecurity()
		{
			return
				CreateForLongOperation(SecurityMode.Message);
		}

		public static ServiceConfiguration CreateForLongOperation()
		{
			return
				CreateForLongOperation(SecurityMode.None);
		}

		public static ServiceConfiguration Create()
		{
			return
				new ServiceConfiguration(
					new NetTcpBinding(),
					HostName,
					Port,
					short.MaxValue,
					short.MaxValue,
					false);
		}

		private static ServiceConfiguration CreateForLongOperation(SecurityMode securityMode)
		{
			return
				new ServiceConfiguration(
					new NetTcpBinding(securityMode),
					HostName,
					Port,
					int.MaxValue,
					int.MaxValue,
					true);
		}
	}
}