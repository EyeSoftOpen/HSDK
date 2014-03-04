namespace EyeSoft.ServiceModel
{
	using System.Security.Cryptography.X509Certificates;
	using System.ServiceModel.Description;
	using System.ServiceModel.Security;

	public static class ClientCredentialsExtensions
	{
		public static void SetCredential(
			this ClientCredentials credentials, string username, string password, bool avoidCertificateValidation = false)
		{
			credentials.UserName.UserName = username;
			credentials.UserName.Password = password;

			if (avoidCertificateValidation)
			{
				credentials.AvoidCertificateValidation();
			}
		}

		public static void AvoidCertificateValidation(this ClientCredentials credentials)
		{
			credentials.ServiceCertificate.Authentication.CertificateValidationMode = X509CertificateValidationMode.None;
			credentials.ServiceCertificate.Authentication.RevocationMode = X509RevocationMode.NoCheck;
		}
	}
}