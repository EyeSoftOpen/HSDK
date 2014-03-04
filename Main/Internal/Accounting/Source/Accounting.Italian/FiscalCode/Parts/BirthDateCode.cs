namespace EyeSoft.Accounting.Italian
{
	using System;

	public class DayCode : Code
	{
		private readonly DateTime birthDate;

		internal DayCode(DateTime birthDate, Sex sex)
		{
			this.birthDate = birthDate;
			Sex = sex;
		}

		public Sex Sex { get; private set; }

		protected internal override string GetCode()
		{
			var sexCode =
				Sex == Sex.Male ? 0 : 40;

			return (birthDate.Day + sexCode).ToInvariant().PadLeft(2, '0');
		}
	}
}