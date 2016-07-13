namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using EyeSoft.FluentValidation;

    using global::FluentValidation;

    public class SimpleViewModelValidator : FluentValidator<SimpleViewModel>
    {
        public SimpleViewModelValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
        }
    }
}