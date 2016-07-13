namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using EyeSoft.FluentValidation;

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