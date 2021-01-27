namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
	public class ControlCode : Code
	{
		private readonly string partialCode;

		public ControlCode(
			LastNameCode lastNameCode,
			FirstNameCode firstNameCode,
			YearCode yearCode,
			MonthCode monthCode,
			DayCode dayCode,
			AreaCode areaCode)
		{
			partialCode = string.Concat(lastNameCode, firstNameCode, yearCode, monthCode, dayCode, areaCode);
		}

		public char Char
		{
			get { return GetControlCode(partialCode); }
		}

		internal static char GetControlCode(string partialWithoutControlCode)
		{
			var sum = 0;

			sum += new OddCode(partialWithoutControlCode).Total;
			sum += new EvenCode(partialWithoutControlCode).Total;

			var controlCode = new RestCode(sum).ToString();

			return controlCode[0];
		}

		protected internal override string GetCode()
		{
			var controlCode = GetControlCode(partialCode);

			return new string(new[] { controlCode });
		}
	}
}