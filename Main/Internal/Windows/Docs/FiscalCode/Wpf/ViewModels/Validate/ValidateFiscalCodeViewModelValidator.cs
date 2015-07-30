namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels
{
	using EyeSoft.Accounting.Italian;
	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	public class ValidateFiscalCodeViewModelValidator : FluentValidator<ValidateFiscalCodeViewModel>
	{
		public ValidateFiscalCodeViewModelValidator()
		{
			RuleFor(x => x.FiscalCode).Must(ValidateFiscalCode);
		}

		private bool ValidateFiscalCode(ValidateFiscalCodeViewModel viewModel, string fiscalCode)
		{
			return string.IsNullOrWhiteSpace(fiscalCode) || new FiscalCodeValidator().Validate(fiscalCode);
		}
	}
}