namespace EyeSoft.Calendar
{
	using System;

	public class AgnosticDay : IComparable<AgnosticDay>
	{
		public AgnosticDay(int day, int month)
		{
			Ensure.That(day).Is.Between(1, 31);
			Ensure.That(month).Is.Between(1, 12);

			Day = day;
			Month = month;
		}

		public int Day { get; private set; }

		public int Month { get; private set; }

		public int CompareTo(AgnosticDay other)
		{
			if (Month > other.Month)
			{
				return 1;
			}

			if (Month < other.Month)
			{
				return -1;
			}

			return Day.CompareTo(other.Day);
		}

		public override string ToString()
		{
			return
				"Day: {Day} Month: {Month}"
					.NamedFormat(Day, Month);
		}
	}
}