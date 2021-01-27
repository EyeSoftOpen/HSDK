namespace EyeSoft.Core.Net.NetworkInformation
{
    using System;
    using System.Net.Sockets;

    public class NetTcpPing : INetTcpPing
	{
		public bool Send(string hostName, int port = 80, bool throwOnError = true, int timeout = 100)
		{
			try
			{
				using (var tcpClient = new TcpClient())
				{
					var asyncResult = tcpClient.BeginConnect(hostName, port, null, null);
					var asyncWaitHandle = asyncResult.AsyncWaitHandle;

					try
					{
						if (!asyncResult.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(timeout), false))
						{
							tcpClient.Close();

							if (throwOnError)
							{
								throw new TimeoutException();
							}

							return false;
						}

						try
						{
							tcpClient.EndConnect(asyncResult);
						}
						catch
						{
							if (throwOnError)
							{
								throw;
							}

							return false;
						}

						return true;
					}
					finally
					{
						asyncWaitHandle.Close();
					}
				}
			}
			catch
			{
				if (throwOnError)
				{
					throw;
				}

				return false;
			}
		}
	}
}