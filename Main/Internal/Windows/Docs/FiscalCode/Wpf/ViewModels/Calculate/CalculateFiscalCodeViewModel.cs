namespace EyeSoft.Demo.FiscalCode.Windows.ViewModels
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Accounting;
	using EyeSoft.Accounting.Italian;
	using EyeSoft.Accounting.Italian.Istat;
	using EyeSoft.Validation;
	using EyeSoft.Windows.Model;

	public class CalculateFiscalCodeViewModel : AutoRegisterViewModel
	{
		public CalculateFiscalCodeViewModel()
		{
			SuspendPropertyChanged();

			Birthdate = DateTime.Now;

			Property(() => FiscalCode)
				.DependsFrom(() => FirstName)
				.DependsFrom(() => TownSearch)
				.DependsFrom(() => Birthdate)
				.DependsFrom(() => Sex)
				.DependsFrom(() => LastName);

			Property(() => TownSearch).OnChanged(x => Town = TownsByName(x).FirstOrDefault());

			ResumePropertyChanged();
		}

		public string FirstName
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public string LastName
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public string TownSearch
		{
			get { return GetProperty<string>(); }
			set { SetProperty(value); }
		}

		public DateTime Birthdate
		{
			get { return GetProperty<DateTime>(); }
			set { SetProperty(value); }
		}

		public int Sex
		{
			get { return GetProperty<int>(); }
			set { SetProperty(value); }
		}

		public string FiscalCode
		{
			get
			{
				if ((!IsValid) || (!Changed) || (Town == null))
				{
					return null;
				}

				var sex = (Sex)Sex;

				var person = new NaturalPerson(FirstName, LastName, Birthdate, sex);
				var fiscalCode = new FiscalCodeProvider().Calculate(person, new AreaCode(Town.Area));

				return fiscalCode.ToString();
			}
		}

		public Town Town { get; private set; }

		public IEnumerable<string> Towns
		{
			get
			{
				return new CityRepository().AllTown().Select(x => x.Name).ToArray();
			}
		}

		protected override IEnumerable<ValidationError> Validate()
		{
			if (!Changed)
			{
				return Enumerable.Empty<ValidationError>();
			}

			var errors = new CalculateFiscalCodeViewModelValidator().Validate(this);

			return errors;
		}

		private IEnumerable<Town> TownsByName(string townName)
		{
			var towns = new CityRepository().TownsByName(townName);

			return towns;
		}
	}
}