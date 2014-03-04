namespace EyeSoft.Windows.Model.Demo.ViewModels.Validators
{
	using System;
	using System.Linq;

	using EyeSoft.FluentValidation;

	using global::FluentValidation;

	internal class MainViewModelValidator : FluentValidator<MainViewModel>
	{
		public MainViewModelValidator()
		{
			RuleFor(x => x.FullName).Must(FullNameCheck).WithMessage("Name must not be empty and must contain name and last name.");
		}

		private bool FullNameCheck(MainViewModel viewModel, string fullName)
		{
			if (viewModel.Changes <= 1)
			{
				return true;
			}

			var notEmptyAndContainsSpace = !string.IsNullOrWhiteSpace(fullName) && fullName.Count(char.IsWhiteSpace) == 1;

			if (!notEmptyAndContainsSpace)
			{
				return false;
			}

			var fullNameCheck = fullName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length == 2;

			return fullNameCheck;
		}
	}
}