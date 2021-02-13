namespace EyeSoft.Accounting.Italian.FiscalCode
{
    using System;
    using EyeSoft;
    using EyeSoft.Extensions;
    using Parts;

    public class RevertedFiscalCode : CalculatedFiscalCode
	{
		public RevertedFiscalCode(string code)
		{
			IsValid = Validate(code);
		}

		public bool IsValid { get; }

		public DateTime? BirthDate { get; private set; }

		private bool Validate(string code)
		{
			if (!new FiscalCodeValidator().Validate(code))
			{
				return false;
			}

			var tokens =
				code
					.Token(FiscalCodeTokens.Lastname, 3)
					.Token(FiscalCodeTokens.Firstname, 3)
					.Token(FiscalCodeTokens.Year, 2)
					.Token(FiscalCodeTokens.Month, 1)
					.Token(FiscalCodeTokens.Day, 2)
					.Token(FiscalCodeTokens.Area, 4)
					.Token(FiscalCodeTokens.Control, 1)
					.Tokens;

			LastName = new LastNameCode(tokens[FiscalCodeTokens.Lastname]).ToString();
			FirstName = new FirstNameCode(tokens[FiscalCodeTokens.Firstname]).ToString();
			AssignBirthDateAndSex(tokens);
			Area = new AreaCode(tokens[FiscalCodeTokens.Area]).ToString();
			Control = ControlCode.GetControlCode(code.Substring(0, 15)).ToInvariant();

			return true;
		}

		private void AssignBirthDateAndSex(IndexedTokens tokens)
		{
			BirthDate = CalculateBirthdate(tokens);

			Year = new YearCode(BirthDate.Value).ToString();
			Month = new MonthCode(BirthDate.Value).ToString();
			Day = new DayCode(BirthDate.Value, Sex).ToString();
		}

		private DateTime CalculateBirthdate(IndexedTokens tokens)
		{
			var year = int.Parse(tokens[FiscalCodeTokens.Year]);
			var month = MonthCode.ToNumber(tokens[FiscalCodeTokens.Month][0]);
			var day = int.Parse(tokens[FiscalCodeTokens.Day]);

			if (day < 40)
			{
				Sex = Sex.Male;
			}
			else
			{
				day -= 40;
				Sex = Sex.Female;
			}

			var dateTime = new DateTime(1900 + year, month, day);
			return dateTime;
		}
	}
}