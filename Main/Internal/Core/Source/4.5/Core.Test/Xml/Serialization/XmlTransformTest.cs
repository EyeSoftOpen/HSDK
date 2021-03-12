namespace EyeSoft.Core.Test.Xml.Serialization
{
    using System.Reflection;
    using EyeSoft.Reflection;
    using EyeSoft.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class XsltTransformerTest
	{
		[TestMethod]
		public void TransformXmlUsingXslt()
		{
			var xsl =
				Assembly
					.GetExecutingAssembly()
					.ReadResourceText("EyeSoft.Core.Test.Xml.Serialization.PersonToHtml.xslt", true);

			var tranformedXml =
				new XsltTransformer()
					.Transform(xsl, XmlSerializerTest.ExpectedXml);

			var expected =
				string
					.Format(
						"<html>\r\n\t<body>\r\n\t\t<div>\r\n\t{0}\r\n</div>\r\n\t</body>\r\n</html>",
						XmlSerializerTest.Title1);

			tranformedXml
				.Should()
				.Be(expected);
		}
	}
}