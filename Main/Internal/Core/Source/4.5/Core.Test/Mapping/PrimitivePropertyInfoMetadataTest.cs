namespace EyeSoft.Test.Mapping
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Mapping;
	using EyeSoft.Testing.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class PrimitivePropertyInfoMetadataTest
	{
		[TestMethod]
		public void VerifyRequiredAttributeIsRetrievedFromProperty()
		{
			var propertyInfo =
				Mocking.Property<string>("Test", new RequiredAttribute());

			var primitivePropertyInfoMetadata = new PrimitiveMemberInfoMetadata(propertyInfo);

			primitivePropertyInfoMetadata
				.Required.Should().Be.True();

			primitivePropertyInfoMetadata
				.Length.HasValue.Should().Be.False();
		}

		[TestMethod]
		public void VerifyStringLengthAttributeIsRetrievedFromProperty()
		{
			const int ExpectedLength = 50;

			var propertyInfo =
				Mocking.Property<string>("Test", new StringLengthAttribute(ExpectedLength));

			var primitivePropertyInfoMetadata = new PrimitiveMemberInfoMetadata(propertyInfo);

			primitivePropertyInfoMetadata
				.SupportLength.Should().Be.True();

			primitivePropertyInfoMetadata
				.Length.Should().Be.EqualTo(ExpectedLength);
		}

		[TestMethod]
		public void VerifyReferencePropertyThrowAnExceptionIfPassingPrimitiveProperty()
		{
			var propertyInfo =
				Mocking.Property<string>("Test");

			Executing
				.This(() => new ReferenceMemberInfoMetadata(propertyInfo))
				.Should().Throw<ArgumentException>();
		}
	}
}