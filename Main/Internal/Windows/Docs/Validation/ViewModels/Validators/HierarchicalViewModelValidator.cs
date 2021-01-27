namespace EyeSoft.Demo.Validation.Windows.ViewModels.Validators
{
    using FluentValidation;

    public class HierarchicalViewModelValidator : FluentValidator<HierarchicalViewModel>
    {
        public HierarchicalViewModelValidator()
        {
            RuleFor(x => x.Subject).SetValidator(new SubjectViewModelValidator());
        }
    }
}