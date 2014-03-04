namespace EyeSoft.Windows.Model.Demo.ViewModels.Validators
{
	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	internal class CustomerViewModelValidator : FluentValidator<CustomerViewModel>
	{
		public CustomerViewModelValidator()
		{
			RuleFor(x => x.FirstName).Length(3, 10);
			RuleFor(x => x.LastName).Length(3, 10);
		}
	}
}