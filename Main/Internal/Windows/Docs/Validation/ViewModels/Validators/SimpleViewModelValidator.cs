namespace EyeSoft.Demo.Validation.Windows.ViewModels.Validators
{
    using FluentValidation;
    using global::FluentValidation;

    public class SimpleViewModelValidator : FluentValidator<SimpleViewModel>
    {
        public SimpleViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
        }
    }
}