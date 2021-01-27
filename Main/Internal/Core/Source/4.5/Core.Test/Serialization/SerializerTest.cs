namespace EyeSoft.Core.Test.Serialization
{
    using Core.Serialization;
    using Core.Xml.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class SerializerTest
	{
		[TestMethod]
		public void SetSerializerVerifyNameIsCorrect()
		{
			Serializer.Set<XmlSerializerFactory>();

			Serializer.Name.Should().Be.EqualTo(XmlSerializerFactory.Name);
		}
	}
}