namespace EyeSoft.Xml.Serialization
{
	using System.IO;
	using System.Xml;
	using System.Xml.Xsl;

	public class XsltTransformer
	{
		private readonly XslCompiledTransform xslTransform =
			new XslCompiledTransform();

		public string Transform(string xsl, string xml)
		{
			using (var xslt = XmlReader.Create(new StringReader(xsl)))
			{
				xslTransform.Load(xslt);
			}

			using (var writer = new StringWriter())
			{
				using (var xmlWriter = XmlWriter.Create(writer, FormattedXmlWriterSettings.Settings))
				{
					var input = XmlReader.Create(new StringReader(xml));

					xslTransform.Transform(input, null, xmlWriter);
					return writer.ToString();
				}
			}
		}
	}
}