namespace EyeSoft.Test.Mapping.Conventions
{
	using System;
	using System.ComponentModel.DataAnnotations;

	using EyeSoft.Mapping;
	using EyeSoft.Mapping.Conventions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class KeyConventionTest
	{
		[TestMethod]
		public void PrimitiveMemberOfNotValidWithTypeKeyAttributeIsNotAValidKey()
		{
			CanBeAKeyWithAttribute<string>("Test", false);
		}

		[TestMethod]
		public void PrimitiveMemberOfValidTypeWithKeyAttributeIsAValidKey()
		{
			CanBeAKeyWithAttribute<Guid>("Test", true);
		}

		[TestMethod]
		public void PrimitiveMemberOfValidTypeWithAllowedNameIsAValidKey()
		{
			CanBeAKeyWithoutAttribute<Guid>("Id", true);
		}

		[TestMethod]
		public void PrimitiveMemberOfNotValidTypeWithAllowedNameIsNotAValidKey()
		{
			CanBeAKeyWithoutAttribute<string>("Id", false);
		}

		private void CanBeAKeyWithAttribute<T>(string memberName, bool expectedIsKey)
		{
			CanBeAKey<T>(memberName, expectedIsKey, new KeyAttribute());
		}

		private void CanBeAKeyWithoutAttribute<T>(string memberName, bool expectedIsKey)
		{
			CanBeAKey<T>(memberName, expectedIsKey, null);
		}

		private void CanBeAKey<T>(string memberName, bool expectedIsKey, Attribute attribute)
		{
			var memberInfo =
				Mocking.Property<T>(memberName, attribute);

			new KeyConvention()
				.CanBeTheKey(new MemberInfoMetadata(memberInfo))
				.Should().Be.EqualTo(expectedIsKey);
		}
	}
}