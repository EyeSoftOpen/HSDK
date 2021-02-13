namespace EyeSoft.Security.Cryptography.X509Certificates
{
    using System.Linq;
    using System.Security.Cryptography.X509Certificates;

    public class X509CertificateSearch
	{
		public static X509Certificate2 FindByFriendlyName(
			string friendlyName, StoreName storeName = StoreName.My, StoreLocation storeLocation = StoreLocation.LocalMachine)
		{
			var store = new X509Store(storeName, storeLocation);
			store.Open(OpenFlags.ReadOnly);
			var certificate = store.Certificates.Cast<X509Certificate2>().Single(x => x.FriendlyName == friendlyName);
			store.Close();

			return certificate;
		}
	}
}