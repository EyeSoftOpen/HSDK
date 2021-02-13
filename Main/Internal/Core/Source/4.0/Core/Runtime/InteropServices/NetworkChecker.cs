namespace EyeSoft.Runtime.InteropServices
{
    using System.Runtime.InteropServices;

    public class NetworkChecker : INetworkChecker
	{
		bool INetworkChecker.IsInternetAvailable
		{
			get
			{
				int description;
				return InternetGetConnectedState(out description, 0);
			}
		}

		[DllImport("wininet.dll")]
		private static extern bool InternetGetConnectedState(out int description, int reeservedValue);
	}
}