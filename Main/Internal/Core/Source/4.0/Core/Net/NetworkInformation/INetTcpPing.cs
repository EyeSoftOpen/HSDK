namespace EyeSoft.Net.NetworkInformation
{
	public interface INetTcpPing
	{
		bool Send(string hostName, int port, bool throwOnError, int timeout);
	}
}