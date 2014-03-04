namespace EyeSoft.ServiceModel.Description
{
	using System;
	using System.IdentityModel.Selectors;
	using System.Security.Cryptography.X509Certificates;
	using System.ServiceModel.Description;
	using System.ServiceModel.Security;

	using EyeSoft.Security.Cryptography.X509Certificates;

	public static class ServiceBehaviors
	{
		public static IServiceBehavior ServiceCredentials(string certificateFriendlyName, Func<UserNamePasswordValidator> validator)
		{
			var certificate = X509CertificateSearch.FindByFriendlyName(certificateFriendlyName);

			return ServiceCredentials(certificate, validator);
		}

		public static IServiceBehavior ServiceCredentials(X509Certificate2 certificate, Func<UserNamePasswordValidator> validator)
		{
			var serviceCredentials = new ServiceCredentials();
			serviceCredentials.ServiceCertificate.Certificate = certificate;

			serviceCredentials.UserNameAuthentication.UserNamePasswordValidationMode = UserNamePasswordValidationMode.Custom;
			serviceCredentials.UserNameAuthentication.CustomUserNamePasswordValidator = validator();

			return serviceCredentials;
		}
	}
}