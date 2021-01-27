namespace EyeSoft.Core.Net.NetworkInformation
{
	public interface INetTcpPing
	{
		bool Send(string hostName, int port, bool throwOnError, int timeout);
	}
}