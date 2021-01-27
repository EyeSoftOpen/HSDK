namespace EyeSoft.Demo.Validation.Windows.ViewModels.Validators
{
    using FluentValidation;
    using global::FluentValidation;

    public class AddressViewModelValidator : FluentValidator<AddressViewModel>
    {
        public AddressViewModelValidator()
        {
            RuleFor(x => x.Street).NotEmpty();

            RuleFor(x => x.City).NotEmpty();
        }
    }
}