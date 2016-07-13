namespace EyeSoft.Demo.Validation.Windows.ViewModels
{
    using EyeSoft.FluentValidation;

    public class HierarchicalViewModelValidator : FluentValidator<HierarchicalViewModel>
    {
        public HierarchicalViewModelValidator()
        {
            RuleFor(x => x.Subject).SetValidator(new SubjectViewModelValidator());
        }
    }
}