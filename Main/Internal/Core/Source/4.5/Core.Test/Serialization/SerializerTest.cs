namespace EyeSoft.Core.Test.Serialization
{
    using EyeSoft.Serialization;
    using EyeSoft.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class SerializerTest
	{
		[TestMethod]
		public void SetSerializerVerifyNameIsCorrect()
		{
			Serializer.Set<XmlSerializerFactory>();

			Serializer.Name.Should().Be(XmlSerializerFactory.Name);
		}
	}
}