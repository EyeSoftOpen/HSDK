namespace EyeSoft.Test.Runtime.Serialization
{
	using System;

	using EyeSoft.Runtime.Serialization;
	using EyeSoft.Serialization;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class BinarySerializerTest
	{
		[TestMethod]
		public void VerifyCloneUsingBinaryFormatterWorks()
		{
			var serializer = new BinarySerializer<Foo>();

			var original = new Foo { Property = "Value1" };
			var cloned = serializer.Clone(original);

			cloned.Property.Should().Be.EqualTo(original.Property);

			cloned.Should().Not.Be.SameInstanceAs(original);
		}

		[Serializable]
		public class Foo
		{
			public string Property { get; set; }
		}
	}
}