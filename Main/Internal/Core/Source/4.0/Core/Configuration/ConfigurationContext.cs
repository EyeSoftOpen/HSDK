namespace EyeSoft.Core.Configuration
{
    using System;
    using System.Collections.Specialized;
    using System.Configuration;

    public static class ConfigurationContext
	{
		private static readonly Singleton<IConfigurationContext> configuration =
			new Singleton<IConfigurationContext>(() => new FileConfigurationContext());

		public static NameValueCollection AppSettings
		{
			get { return configuration.Instance.AppSettings; }
		}

		public static ConnectionStringSettingsCollection ConnectionStrings
		{
			get { return configuration.Instance.ConnectionStrings; }
		}

		public static void Set(Func<IConfigurationContext> createConfigurationContext)
		{
			configuration.Set(createConfigurationContext);
		}

		public static void Set(IConfigurationContext configurationContext)
		{
			configuration.Set(configurationContext);
		}
	}
}