namespace EyeSoft.Test.Validation
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Validation;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ValidatorTest
	{
		public const string PropertyEmpty = "Should not be empty.";

		public const string PropertyTooShort = "Too short.";

		public const string NameProperty = "Name";

		public const string AddressProperty = "Address";

		private readonly IValidator<ValidableCustomer> validator;

		public ValidatorTest() : this(new CustomerValidator())
		{
		}

		public ValidatorTest(IValidator<ValidableCustomer> validator)
		{
			this.validator = validator;
		}

		[TestMethod]
		public void CheckValidPropertyExpectedNoErrors()
		{
			var customer = new ValidableCustomer { Name = NameProperty };

			validator.Validate(customer, NameProperty).Should().Be.Empty();
		}

		[TestMethod]
		public void CheckNotValidPropertyExpectedSingleError()
		{
			const string Name = "B";
			var customer = new ValidableCustomer { Name = Name };

			validator
				.Validate(customer, NameProperty)
				.Should().Have.SameSequenceAs(new ValidationError(NameProperty, PropertyTooShort, Name));
		}

		[TestMethod]
		public void CheckNotValidPropertyExpectedError()
		{
			var customer = new ValidableCustomer { Name = string.Empty };

			validator
				.Validate(customer, NameProperty)
				.Should().Have.SameSequenceAs(
					new ValidationError(NameProperty, PropertyEmpty, string.Empty),
					new ValidationError(NameProperty, PropertyTooShort, null));
		}

		[TestMethod]
		public void CheckValidPropertiesExpectedNoErrors()
		{
			var customer = new ValidableCustomer { Name = NameProperty, Address = AddressProperty };

			validator.Validate(customer).Should().Be.Empty();
		}

		[TestMethod]
		public void CheckNotValidPropertiesExpectedError()
		{
			var customer = new ValidableCustomer();

			var validationErrors = validator.Validate(customer);

			var expectedValidationErrors =
				new[]
					{
						new ValidationError(NameProperty, PropertyEmpty, null),
						new ValidationError(AddressProperty, PropertyEmpty, null)
					};

			validationErrors.Should().Have.SameSequenceAs(expectedValidationErrors);
		}

		public class ValidableCustomer
		{
			public string Name { get; set; }

			public string Address { get; set; }
		}

		private class CustomerValidator : Validator<ValidableCustomer>
		{
			public override IEnumerable<ValidationError> Validate(ValidableCustomer instance, string propertyName)
			{
				if (propertyName == NameProperty)
				{
					var errors = Validate(propertyName, instance.Name, NameProperty.Length).ToList();

					return errors.Any() ? errors : Enumerable.Empty<ValidationError>();
				}

				if (propertyName == AddressProperty)
				{
					var errors = Validate(propertyName, instance.Address, AddressProperty.Length).ToList();

					return errors.Any() ? errors : Enumerable.Empty<ValidationError>();
				}

				throw new ArgumentException("Property name not valid.");
			}

			private IEnumerable<ValidationError> Validate(string propertyName, string value, int length)
			{
				if (string.IsNullOrWhiteSpace(value))
				{
					yield return new ValidationError(propertyName, PropertyEmpty, value);
				}

				if ((value != null) && (value.Length < length))
				{
					yield return new ValidationError(propertyName, PropertyTooShort, value);
				}
			}
		}
	}
}