namespace EyeSoft.Demo.Validation.Windows.ViewModels.Validators
{
    using FluentValidation;
    using global::FluentValidation;

    public class SubjectViewModelValidator : FluentValidator<SubjectViewModel>
    {
        public SubjectViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();

            RuleFor(x => x.Address).SetValidator(new AddressViewModelValidator());
        }
    }
}