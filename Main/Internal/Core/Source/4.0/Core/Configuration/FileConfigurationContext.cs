namespace EyeSoft.Configuration
{
	using System.Collections.Specialized;
	using System.Configuration;

	internal class FileConfigurationContext : IConfigurationContext
	{
		public NameValueCollection AppSettings
		{
			get { return ConfigurationManager.AppSettings; }
		}

		public ConnectionStringSettingsCollection ConnectionStrings
		{
			get { return ConfigurationManager.ConnectionStrings; }
		}
	}
}