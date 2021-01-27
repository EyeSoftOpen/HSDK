namespace EyeSoft.Core.Runtime.InteropServices
{
	public interface INetworkChecker
	{
		bool IsInternetAvailable { get; }
	}
}