namespace EyeSoft.FluentValidation.Test
{
	using System.Collections.Generic;

	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	using global::FluentValidation.Internal;

	using global::FluentValidation.Results;

	using global::FluentValidation.Validators;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class OnlyCharactersValidatorTest
	{
		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithNullTextNotReturnsErrors()
		{
			var result = Validate(null);
			result.Should().Be.Empty();
		}

		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithTextNotReturnsErrors()
		{
			var result = Validate("Abc");
			result.Should().Be.Empty();
		}

		[TestMethod]
		public void VerifyOnlyCharactersValidatorWithWrongTextReturnsErrors()
		{
			var result = Validate("Abc1");
			result.Should().Not.Be.Empty();
		}

		private static IEnumerable<ValidationFailure> Validate(object value)
		{
			var parentContext = new ValidationContext(null);
			var rule = new PropertyRule(null, x => value, null, null, typeof(string), null) { PropertyName = "Name" };
			var context = new PropertyValidatorContext(parentContext, rule, null);
			var validator = new OnlyCharactersValidator();
			var result = validator.Validate(context);
			return result;
		}
	}
}