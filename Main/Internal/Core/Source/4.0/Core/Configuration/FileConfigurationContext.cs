namespace EyeSoft.Configuration
{
    using System.Collections.Specialized;
    using System.Configuration;

    internal class FileConfigurationContext : IConfigurationContext
	{
		public NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        public ConnectionStringSettingsCollection ConnectionStrings => ConfigurationManager.ConnectionStrings;
    }
}