namespace EyeSoft.Test.Mapping
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Mapping;
	using EyeSoft.Test.Helpers;
	using EyeSoft.Testing.Reflection;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ReferencePropertyInfoMetadataTest
	{
		[TestMethod]
		public void VerifyReferencePropertyThrowAnExceptionIfPassingPrimitiveProperty()
		{
			var propertyInfo =
				Mocking.Property<string>("Test");

			Executing
				.This(() => new ReferenceMemberInfoMetadata(propertyInfo))
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void VerifyReferencePropertyWithoutRequiredAttributeIsNotRequired()
		{
			var propertyInfo =
				Mocking.Property<Address>("Test");

			new ReferenceMemberInfoMetadata(propertyInfo)
				.Required.Should().Be.False();
		}

		[TestMethod]
		public void VerifyReferencePropertyWithRequiredAttributeIsRequired()
		{
			var propertyInfo =
				Mocking.Property<Address>("Test", new RequiredAttribute());

			new ReferenceMemberInfoMetadata(propertyInfo)
				.Required.Should().Be.True();
		}
	}
}