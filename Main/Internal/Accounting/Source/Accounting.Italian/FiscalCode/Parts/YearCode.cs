namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
    using System;

    public class YearCode : Code
	{
		private readonly DateTime birthDate;

		public YearCode(DateTime birthDate)
		{
			this.birthDate = birthDate;
		}

		protected internal override string GetCode()
		{
			return birthDate.ToString("yy");
		}
	}
}