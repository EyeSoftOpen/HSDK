namespace EyeSoft.Core.Net.Mail
{
    using System.Net;
    using System.Net.Mail;
    using System.Security.Cryptography.X509Certificates;

    internal class SmtpClientWrapper : ISmtpClient
	{
		private readonly SmtpClient smtpClient = new SmtpClient();

		public SmtpDeliveryMethod DeliveryMethod
		{
			get { return smtpClient.DeliveryMethod; }
			set { smtpClient.DeliveryMethod = value; }
		}

		public ICredentialsByHost Credentials
		{
			get { return smtpClient.Credentials; }
			set { smtpClient.Credentials = value; }
		}

		public X509CertificateCollection ClientCertificates
		{
			get { return smtpClient.ClientCertificates; }
		}

		public ServicePoint ServicePoint
		{
			get { return smtpClient.ServicePoint; }
		}

		public string TargetName
		{
			get { return smtpClient.TargetName; }
			set { smtpClient.TargetName = value; }
		}

		public int Timeout
		{
			get { return smtpClient.Timeout; }
			set { smtpClient.Timeout = value; }
		}

		public bool UseDefaultCredentials
		{
			get { return smtpClient.UseDefaultCredentials; }
			set { smtpClient.UseDefaultCredentials = value; }
		}
		
		public void Send(MailMessage mailMessage)
		{
			smtpClient.Send(mailMessage);
		}

		public void Dispose()
		{
			smtpClient.Dispose();
		}
	}
}