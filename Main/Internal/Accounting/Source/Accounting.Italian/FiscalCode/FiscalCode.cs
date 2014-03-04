namespace EyeSoft.Accounting.Italian
{
	public class FiscalCode : CalculatedFiscalCode
	{
		private readonly string code;

		internal FiscalCode(
			LastNameCode lastNameCode,
			FirstNameCode firstNameCode,
			YearCode yearCode,
			MonthCode monthCode,
			DayCode dayCode,
			AreaCode areaCode,
			ControlCode controlCode)
			: this(
				lastNameCode.ToString(),
				firstNameCode.ToString(),
				yearCode.ToString(),
				monthCode.ToString(),
				dayCode.ToString(),
				areaCode.ToString(),
				controlCode.ToString())
		{
		}

		internal FiscalCode(
			string lastNameCode,
			string firstNameCode,
			string yearCode,
			string monthCode,
			string dayCode,
			string areaCode,
			string controlCode)
		{
			LastName = lastNameCode.ToUpper();
			FirstName = firstNameCode.ToUpper();
			Year = yearCode.ToUpper();
			Month = monthCode.ToUpper();
			Day = dayCode.ToUpper();
			Area = areaCode.ToUpper();
			Control = controlCode.ToUpper();

			code = string.Concat(LastName, FirstName, Year, Month, Day, Area, Control);

			IsValid = true;
		}

		public bool IsValid { get; private set; }

		public override string ToString()
		{
			return code;
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (FiscalCode)obj;
			return code.Equals(other.code);
		}

		public override int GetHashCode()
		{
			return code.GetHashCode();
		}
	}
}