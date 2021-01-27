namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels.Calculate
{
    using System;
    using FluentValidation;
    using global::FluentValidation;

    public class CalculateFiscalCodeViewModelValidator : FluentValidator<CalculateFiscalCodeViewModel>
	{
		public CalculateFiscalCodeViewModelValidator()
		{
			RuleFor(x => x.FirstName).NotNull().Length(3, 20).OnlyCharacters();
			RuleFor(x => x.LastName).NotNull().Length(3, 30).OnlyCharacters();

			RuleFor(x => x.TownSearch).Must(TownSearchNotEmptyAndValid).WithMessage("Town is not valid.");

			RuleFor(x => x.Birthdate).GreaterThan(new DateTime(1900, 1, 1));
		}

		private bool TownSearchNotEmptyAndValid(CalculateFiscalCodeViewModel viewModel, string townSearch)
		{
			var valid = string.IsNullOrWhiteSpace(townSearch) || viewModel.Town != null;

			return valid;
		}
	}
}