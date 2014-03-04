namespace EyeSoft.Web.Http
{
	using System.Linq;
	using System.Web.Http;

	public static class HttpConfigurationExtensions
	{
		public static void RemoveXmlFormatterToUseJsonFormatter(this HttpConfiguration configuration)
		{
			var appXmlType =
				configuration.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");

			configuration.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
		}
	}
}