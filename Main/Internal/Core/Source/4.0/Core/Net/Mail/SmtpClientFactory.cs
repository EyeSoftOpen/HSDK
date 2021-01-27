namespace EyeSoft.Core.Net.Mail
{
    using System;

    public static class SmtpClientFactory
	{
		private static readonly Singleton<Func<ISmtpClient>> singletonInstance =
			new Singleton<Func<ISmtpClient>>(() => () => new SmtpClientWrapper());

		public static void Set(Func<ISmtpClient> valueFactory)
		{
			singletonInstance.Set(valueFactory);
		}

		public static void Reset(Func<ISmtpClient> valueFactory)
		{
			singletonInstance.Set(valueFactory);
		}

		public static ISmtpClient Create()
		{
			return singletonInstance.Instance();
		}
	}
}