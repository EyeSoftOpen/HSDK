namespace EyeSoft.Net.Mail
{
	using System;
	using System.Net;
	using System.Net.Mail;
	using System.Security.Cryptography.X509Certificates;

	public interface ISmtpClient : IDisposable
	{
		SmtpDeliveryMethod DeliveryMethod { get; set; }

		ICredentialsByHost Credentials { get; set; }

		X509CertificateCollection ClientCertificates { get; }

		bool UseDefaultCredentials { get; set; }

		ServicePoint ServicePoint { get; }

		string TargetName { get; set; }

		int Timeout { get; set; }

		void Send(MailMessage mailMessage);
	}
}