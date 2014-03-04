namespace EyeSoft.Calendar
{
	using System;
	using System.Collections.Generic;
	using System.Linq;

	internal class HolidayEntries
	{
		private readonly List<Holiday> holidayList = new List<Holiday>();

		private IEnumerable<Holiday> lastHolidaysAdded;

		private bool lastWasFixed;

		public IEnumerable<Holiday> LastHolidaysAdded
		{
			get { return lastHolidaysAdded; }
		}

		public bool LastWasFixed
		{
			get { return lastWasFixed; }
		}

		public void AddRange(IList<Holiday> holidays)
		{
			AddRange(holidays, lastWasFixed);
		}

		public void AddRange(IList<Holiday> holidays, bool isFixed)
		{
			holidayList.AddRange(holidays);

			lastHolidaysAdded = holidays;

			lastWasFixed = isFixed;
		}

		public IEnumerable<Holiday> List()
		{
			return
				holidayList.OrderBy(holiday => holiday.Date);
		}

		public void Check(AgnosticDay agnosticDay)
		{
			if (holidayList.Any(holiday => holiday.Date.Month > agnosticDay.Month))
			{
				new ArgumentException().Throw();
			}

			if (holidayList.Any(holiday => holiday.Date.Month == agnosticDay.Month && holiday.Date.Day > agnosticDay.Day))
			{
				new ArgumentException().Throw();
			}
		}
	}
}