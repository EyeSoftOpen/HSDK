namespace EyeSoft.Accounting.Italian
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	using EyeSoft.Collections.Generic;

	public class MonthCode : Code
	{
		private static readonly IDictionary<int, char> monthToCode =
				new FluentDictionary<int, char>()
					.Entry(1, 'A').Entry(2, 'B').Entry(3, 'C').Entry(4, 'D')
					.Entry(5, 'E').Entry(6, 'H').Entry(7, 'L').Entry(8, 'M')
					.Entry(9, 'P').Entry(10, 'R').Entry(11, 'S').Entry(12, 'T');

		private readonly DateTime birthDate;

		public MonthCode(DateTime birthDate)
		{
			this.birthDate = birthDate;
		}

		protected internal override string GetCode()
		{
			return monthToCode[birthDate.Month].ToInvariant();
		}

		public static int ToNumber(char monthCode)
		{
			return
				monthToCode.Single(m => m.Value == monthCode).Key;
		}
	}
}