namespace EyeSoft.Accounting.Italian.FiscalCode.Parts
{
    using System;
    using EyeSoft.Extensions;

    public class DayCode : Code
	{
		private readonly DateTime birthDate;

		internal DayCode(DateTime birthDate, Sex sex)
		{
			this.birthDate = birthDate;
			Sex = sex;
		}

		public Sex Sex { get; }

		protected internal override string GetCode()
		{
			var sexCode = Sex == Sex.Male ? 0 : 40;

			return (birthDate.Day + sexCode).ToInvariant().PadLeft(2, '0');
		}
	}
}