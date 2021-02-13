namespace EyeSoft.Core.Test.Xml.Serialization
{
    using System.Collections.Generic;
    using EyeSoft.Xml.Serialization;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class XmlSerializerTest
	{
		internal const string Title1 = "Title1";

		internal const string ExpectedXml =
			"<XmlSerializable>\r\n\t<Title>" + Title1 + "</Title>\r\n</XmlSerializable>";

		private const string Title2 = "Title2";

		private const string ExpectedCollectionXml =
			"<XmlSerializableList>\r\n\t" +
			"<XmlSerializable>\r\n\t\t<Title>Title1</Title>\r\n\t</XmlSerializable>\r\n\t" +
			"<XmlSerializable>\r\n\t\t<Title>Title2</Title>\r\n\t</XmlSerializable>\r\n" +
			"</XmlSerializableList>";

		private readonly IEnumerable<XmlSerializable> collection =
			new[]
				{
					new XmlSerializable { Title = Title1 },
					new XmlSerializable { Title = Title2 }
				};

		private readonly XmlSerializer<XmlSerializable> xmlSerializer =
			new XmlSerializer<XmlSerializable>();

		[TestMethod]
		public void SerializeObjectToXmlString()
		{
			var xml = new XmlSerializer<XmlSerializable>().SerializeToString(new XmlSerializable { Title = Title1 });

			xml.Should().Be.EqualTo(ExpectedXml);
		}

		[TestMethod]
		public void SerializeCollectionToXmlString()
		{
			var xml = xmlSerializer.Serialize(collection);

			xml.Should().Be.EqualTo(ExpectedCollectionXml);
		}

		[TestMethod]
		public void DeserializeXmlStringToCollection()
		{
			var deserializedCollection = xmlSerializer.DeserializeCollection(ExpectedCollectionXml);

			deserializedCollection.Should().Have.SameSequenceAs(collection);
		}
	}
}