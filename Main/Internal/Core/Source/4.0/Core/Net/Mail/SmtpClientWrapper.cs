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
			get => smtpClient.DeliveryMethod;
            set => smtpClient.DeliveryMethod = value;
        }

		public ICredentialsByHost Credentials
		{
			get => smtpClient.Credentials;
            set => smtpClient.Credentials = value;
        }

		public X509CertificateCollection ClientCertificates => smtpClient.ClientCertificates;

        public ServicePoint ServicePoint => smtpClient.ServicePoint;

        public string TargetName
		{
			get => smtpClient.TargetName;
            set => smtpClient.TargetName = value;
        }

		public int Timeout
		{
			get => smtpClient.Timeout;
            set => smtpClient.Timeout = value;
        }

		public bool UseDefaultCredentials
		{
			get => smtpClient.UseDefaultCredentials;
            set => smtpClient.UseDefaultCredentials = value;
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