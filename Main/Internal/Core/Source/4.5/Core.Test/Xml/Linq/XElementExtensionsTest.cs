namespace EyeSoft.Core.Test.Xml.Linq
{
    using System.Xml.Linq;
    using Core.Xml.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class XElementExtensionsTest
	{
		[TestMethod]
		public void VerifyXmlNamespacesAreRemoved()
		{
			const string Xml =
				"<Employees xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
				"	<Employee xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">" +
				"		<Id>1</Id>" +
				"		<Name>Bill</Name>" +
				"		</Employee>" +
				"</Employees>";

			const string XmlWithoutNamespaces =
				"<Employees>\r\n  <Employee>\r\n    <Id>1</Id>\r\n    <Name>Bill</Name>\r\n  </Employee>\r\n</Employees>";

			var xml = XElement.Parse(Xml).RemoveNamespaces().ToString();

			xml.Should().Be.EqualTo(XmlWithoutNamespaces);
		}
	}
}