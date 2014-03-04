namespace EyeSoft.Net.NetworkInformation
{
	using System;

	public static class TcpPing
	{
		private static readonly Singleton<INetTcpPing> singletonInstance =
			new Singleton<INetTcpPing>(() => new NetTcpPing());

		public static void Set(Func<INetTcpPing> instance)
		{
			singletonInstance.Set(instance);
		}

		public static bool Send(string hostName, int port = 80, bool throwOnError = false, int timeOut = 3000)
		{
			return singletonInstance.Instance.Send(hostName, port, throwOnError, timeOut);
		}
	}
}