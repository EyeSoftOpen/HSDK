namespace EyeSoft.Core.Test.Mapping
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using EyeSoft.Mapping;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ReferencePropertyInfoMetadataTest
	{
		[TestMethod]
		public void VerifyReferencePropertyThrowAnExceptionIfPassingPrimitiveProperty()
		{
			var propertyInfo = Mocking.Property<string>("Test");

            Action action = () => new ReferenceMemberInfoMetadata(propertyInfo);

            action.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void VerifyReferencePropertyWithoutRequiredAttributeIsNotRequired()
		{
			var propertyInfo =
				Mocking.Property<Address>("Test");

			new ReferenceMemberInfoMetadata(propertyInfo)
				.Required.Should().BeFalse();
		}

		[TestMethod]
		public void VerifyReferencePropertyWithRequiredAttributeIsRequired()
		{
			var propertyInfo =
				Mocking.Property<Address>("Test", new RequiredAttribute());

			new ReferenceMemberInfoMetadata(propertyInfo)
				.Required.Should().BeTrue();
		}
	}
}