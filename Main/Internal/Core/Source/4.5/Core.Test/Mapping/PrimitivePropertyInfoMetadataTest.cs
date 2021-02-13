namespace EyeSoft.Core.Test.Mapping
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Reflection;
    using EyeSoft.Mapping;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
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
			var propertyInfo = Mocking.Property<string>("Test");

			Executing
				.This(() => new ReferenceMemberInfoMetadata(propertyInfo))
				.Should().Throw<ArgumentException>();
		}
	}

	public class Mocking
	{
		public static MockedPropertyInfo Property<T>(string name, params object[] attributes)
		{
			return Property<T>(Visibility.Public, name, attributes);
		}

		public static MockedPropertyInfo PrivateProperty<T>(string name, params object[] attributes)
		{
			return
				Property<T>(Visibility.Public, name, attributes);
		}

		private static MockedPropertyInfo Property<T>(Visibility visibility, string name, params object[] attributes)
		{
			var mock = new Mock<MockedPropertyInfo>();

			mock
				.SetupGet(p => p.Name)
				.Returns(name);

			mock
				.SetupGet(p => p.PropertyType)
				.Returns(typeof(T));

			mock
				.Setup(p => p.GetCustomAttributes(It.IsAny<Type>(), It.IsAny<bool>()))
				.Returns((Type type, bool inherit) => attributes.Where(attribute => attribute.GetType() == type).ToArray());

			mock
				.SetupGet(p => p.MemberType)
				.Returns(MemberTypes.Property);

			return
				mock.Object;
		}
	}

	public abstract class MockedPropertyInfo
	: PropertyInfo
	{
		public IEnumerable<MockedPropertyInfo> Property<T>(string name)
		{
			yield return this;

			yield return Mocking.Property<T>(name);
		}
	}

	public enum Visibility
	{
		Public,
		Private,
		Internal
	}
}