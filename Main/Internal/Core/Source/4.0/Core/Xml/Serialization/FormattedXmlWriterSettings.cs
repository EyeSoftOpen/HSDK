namespace EyeSoft.Xml.Serialization
{
    using System.Xml;

    public static class FormattedXmlWriterSettings
	{
		public static readonly XmlWriterSettings Settings =
			new XmlWriterSettings
				{
					OmitXmlDeclaration = true,
					Indent = true,
					IndentChars = "\t"
				};
	}
}