namespace EyeSoft.FluentValidation.Test.Helpers
{
    using global::FluentValidation;

    internal class CustomerFluentValidator : FluentValidator<ValidatorTest.ValidableCustomer>
	{
		public CustomerFluentValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty().WithMessage(ValidatorTest.PropertyEmpty)
				.Length(ValidatorTest.NameProperty.Length, 100).WithMessage(ValidatorTest.PropertyTooShort);

			RuleFor(x => x.Address)
				.NotEmpty().WithMessage(ValidatorTest.PropertyEmpty)
				.Length(ValidatorTest.AddressProperty.Length, 100).WithMessage(ValidatorTest.PropertyTooShort);
		}
	}
}