namespace EyeSoft.Accounting.Italian
{
	public class FiscalCodeProvider
	{
		public FiscalCode Calculate(NaturalPerson naturalPerson, AreaCode areaCode)
		{
			var lastName = new LastNameCode(naturalPerson.LastName.Replace(" ", null));

			var firstName = new FirstNameCode(naturalPerson.FirstName.Replace(" ", null));

			var year = new YearCode(naturalPerson.BirthDate);

			var month = new MonthCode(naturalPerson.BirthDate);

			var day = new DayCode(naturalPerson.BirthDate, naturalPerson.Sex);

			var control = new ControlCode(lastName, firstName, year, month, day, areaCode);

			var fiscalCode = new FiscalCode(lastName, firstName, year, month, day, areaCode, control);

			return fiscalCode;
		}
	}
}