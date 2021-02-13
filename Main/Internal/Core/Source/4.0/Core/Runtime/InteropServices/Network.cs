namespace EyeSoft.Runtime.InteropServices
{
	public static class Network
	{
		private static INetworkChecker networkChecker = new NetworkChecker();

		public static bool IsInternetAvailable => networkChecker.IsInternetAvailable;

        public static void Set(INetworkChecker checker)
		{
			networkChecker = checker;
		}
	}
}