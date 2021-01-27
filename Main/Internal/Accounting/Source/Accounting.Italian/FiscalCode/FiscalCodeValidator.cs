namespace EyeSoft.Accounting.Italian.FiscalCode
{
    using System.Linq;
    using System.Text.RegularExpressions;
    using Parts;

    public class FiscalCodeValidator
	{
		private static readonly Regex fiscalCodeFormat = new Regex(@"^[A-Z]{6}[\d]{2}[A-Z][\d]{2}[A-Z][\d]{3}[A-Z]$");

		public bool Validate(string fiscalCode)
		{
			if (string.IsNullOrEmpty(fiscalCode) || fiscalCode.Length != 16)
			{
				return false;
			}

			fiscalCode = fiscalCode.ToUpper();

			if (!fiscalCode.All(char.IsLetterOrDigit))
			{
				return false;
			}

			var formatValid = fiscalCodeFormat.Match(fiscalCode).Success;

			if (!formatValid)
			{
				return false;
			}

			var controlCode = fiscalCode[15];

			var calculatedCode = ControlCode.GetControlCode(fiscalCode.Substring(0, 15));

			return calculatedCode == controlCode;
		}
	}
}