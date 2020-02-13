namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels
{
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Accounting;
	using EyeSoft.Accounting.Italian;
	using EyeSoft.Accounting.Italian.Istat;
	using EyeSoft.Validation;
	using EyeSoft.Windows.Model;

	public class ValidateFiscalCodeViewModel : AutoRegisterViewModel
	{
		public ValidateFiscalCodeViewModel()
		{
			Property(() => FirstName).DependsFrom(() => FiscalCode);
			Property(() => LastName).DependsFrom(() => FiscalCode);
			Property(() => Birthdate).DependsFrom(() => FiscalCode);
			Property(() => Sex).DependsFrom(() => FiscalCode);
			Property(() => Town).DependsFrom(() => FiscalCode);

			Property(() => FiscalCode).OnChanged(RevertFiscalCode);
		}

		public string FirstName
		{
			get { return GetProperty<string>(); }
			private set { SetProperty(value); }
		}

		public string LastName
		{
			get { return GetProperty<string>(); }
			private set { SetProperty(value); }
		}

		public string Birthdate
		{
			get { return GetProperty<string>(); }
			private set { SetProperty(value); }
		}

		public Sex? Sex
		{
			get { return GetProperty<Sex?>(); }
			private set { SetProperty(value); }
		}

		public string FiscalCode
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public string Town { get; private set; }

		public override IEnumerable<ValidationError> Validate()
		{
			return new ValidateFiscalCodeViewModelValidator().Validate(this);
		}

		private void RevertFiscalCode(string fiscalCode)
		{
			if (!new FiscalCodeValidator().Validate(fiscalCode))
			{
				CleanFields();
				return;
			}

			var revertedFiscalCode = new RevertedFiscalCode(fiscalCode);

			FirstName = revertedFiscalCode.FirstName;
			LastName = revertedFiscalCode.LastName;
			Sex = revertedFiscalCode.Sex;
			Birthdate = revertedFiscalCode.BirthDate.HasValue ? revertedFiscalCode.BirthDate.Value.ToShortDateString() : null;
			var town = new CityRepository().AllTown().SingleOrDefault(x => x.Area == revertedFiscalCode.Area);
			Town = town == null ? null : town.Name;
		}

		private void CleanFields()
		{
			FirstName = null;
			LastName = null;
			Sex = null;
			Birthdate = null;
			Town = null;
		}
	}
}