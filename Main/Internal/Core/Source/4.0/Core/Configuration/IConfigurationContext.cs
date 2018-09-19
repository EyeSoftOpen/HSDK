namespace EyeSoft.Configuration
{
	using System.Collections.Specialized;
	using System.Configuration;

	public interface IConfigurationContext
	{
		NameValueCollection AppSettings { get; }

		ConnectionStringSettingsCollection ConnectionStrings { get; }
	}
}