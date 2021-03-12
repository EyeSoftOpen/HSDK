namespace EyeSoft.FluentValidation.Test
{
	using System.Collections.Generic;

	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	using global::FluentValidation.Internal;

	using global::FluentValidation.Results;

	using global::FluentValidation.Validators;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	[TestClass]
	public class OnlyCharactersValidatorTest
	{
		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithNullTextNotReturnsErrors()
		{
			var result = Validate((object)null);
			result.Should().BeEmpty();
		}

		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithTextNotReturnsErrors()
		{
			var result = Validate("Abc");
			result.Should().BeEmpty();
		}

		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithWrongTextReturnsErrors()
		{
			var result = Validate("Abc1");
			result.Should().NotBeEmpty();
		}

		private static IEnumerable<ValidationFailure> Validate<T>(T value)
		{
			var parentContext = new ValidationContext<T>(value);
			var rule = new PropertyRule(null, x => value, null, null, typeof(string), null) { PropertyName = "Name" };
			var context = new PropertyValidatorContext(parentContext, rule, "Name", value);
			var validator = new OnlyCharactersValidator();
			var result = validator.Validate(context);
			return result;
		}
	}
}