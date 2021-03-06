﻿namespace EyeSoft.Core.Test.Runtime.Serialization
{
    using System;
    using EyeSoft.Runtime.Serialization;
    using EyeSoft.Serialization;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class BinarySerializerTest
	{
		[TestMethod]
		public void VerifyCloneUsingBinaryFormatterWorks()
		{
			var serializer = new BinarySerializer<Foo>();

			var original = new Foo { Property = "Value1" };
			var cloned = serializer.Clone(original);

			cloned.Property.Should().Be(original.Property);

			cloned.Should().NotBeSameAs(original);
		}

		[Serializable]
		public class Foo
		{
			public string Property { get; set; }
		}
	}
}