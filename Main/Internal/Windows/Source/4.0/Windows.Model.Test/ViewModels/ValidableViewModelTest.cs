namespace EyeSoft.Windows.Model.Test.ViewModels
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Validation;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ValidableViewModelTest
	{
		[TestMethod]
		public void ValidateViewModelWithoutValidationLogicExpectedErrorIsEmpty()
		{
			var simpleViewModel = new SimpleViewModel();
			simpleViewModel.IsValid.Should().Be.True();
			simpleViewModel.Error.Should().Be.Empty();
		}

		[TestMethod]
		public void ValidateNotValidViewModelExpectedError()
		{
			var simpleViewModel = new ValidableViewModel();
			simpleViewModel.IsValid.Should().Be.False();
			simpleViewModel.Error.Should().Be.EqualTo(ValidableViewModel.ValidableViewModelValidator.ExpectedError);
		}

		[TestMethod]
		public void ValidateValidViewModelExpectedNoError()
		{
			var simpleViewModel = new ValidableViewModel { Name = "Ok" };
			simpleViewModel.IsValid.Should().Be.True();
			simpleViewModel.Error.Should().Be.Empty();
		}

		[TestMethod]
		public void ValidateViewModelThatDoesNotOverrideValidateMethodExpectedNoError()
		{
			var simpleViewModel = new ValidableViewModel();
			simpleViewModel.IsValid.Should().Be.False();
			simpleViewModel.Error.Should().Be.EqualTo(ValidableViewModel.ValidableViewModelValidator.ExpectedError);
		}

		private class SimpleViewModel : AutoRegisterViewModel
		{
		}

		private class ValidableViewModel : AutoRegisterViewModel
		{
			public string Name { get; set; }

			public override IEnumerable<ValidationError> Validate()
			{
				return new ValidableViewModelValidator().Validate(this).ToList();
			}

			public class ValidableViewModelValidator : Validator<ValidableViewModel>
			{
				public const string ExpectedError = "Name required.";

				private const string PropertyName = "Name";

				public override IEnumerable<ValidationError> Validate(ValidableViewModel instance, string propertyName)
				{
					if (propertyName != PropertyName)
					{
						yield break;
					}

					if (string.IsNullOrWhiteSpace(instance.Name))
					{
						yield return new ValidationError(PropertyName, ExpectedError, instance.Name);
					}
				}
			}
		}
	}
}