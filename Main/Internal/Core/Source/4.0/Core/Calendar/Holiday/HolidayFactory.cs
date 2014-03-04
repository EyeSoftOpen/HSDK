namespace EyeSoft.Calendar
{
	using System.Collections.Generic;

	public class HolidayFactory
	{
		private readonly Holidays holidays;

		public HolidayFactory(int startingYear, int futureYears = 0)
		{
			holidays = new Holidays(startingYear, futureYears);
		}

		public Holidays Fixed(string name, AgnosticDay agnosticDay)
		{
			return holidays.Fixed(name, agnosticDay);
		}

		public Holidays Entry(string name, params AgnosticDay[] dates)
		{
			return holidays.Entry(name, dates);
		}

		public Holidays NextDay(string name)
		{
			return holidays.NextDay(name);
		}

		public IEnumerable<Holiday> List()
		{
			return
				holidays.List();
		}
	}
}