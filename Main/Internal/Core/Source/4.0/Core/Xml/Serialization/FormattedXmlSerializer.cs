namespace EyeSoft.Xml.Serialization
{
	using System;
	using System.IO;
	using System.Xml;
	using System.Xml.Serialization;

	public class FormattedXmlSerializer : XmlSerializer
	{
		private readonly XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

		public FormattedXmlSerializer()
		{
			ns.Add(string.Empty, string.Empty);
		}

		public FormattedXmlSerializer(Type type) : base(type)
		{
			ns.Add(string.Empty, string.Empty);
		}

		public string Serialize(object obj)
		{
			using (var stringWriter = new StringWriter())
			{
				using (var xmlWriter = XmlWriter.Create(stringWriter, FormattedXmlWriterSettings.Settings))
				{
					Serialize(xmlWriter, obj, ns);

					var xml = stringWriter.ToString();

					return xml;
				}
			}
		}

		public Stream SerializeToStream(object obj, Stream stream)
		{
			using (var xmlWriter = XmlWriter.Create(stream, FormattedXmlWriterSettings.Settings))
			{
				Serialize(xmlWriter, obj, ns);

				return stream;
			}
		}
	}
}