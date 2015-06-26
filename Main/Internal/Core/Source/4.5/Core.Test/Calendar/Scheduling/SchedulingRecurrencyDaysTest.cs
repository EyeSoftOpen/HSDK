namespace EyeSoft.Test.Calendar
{
	using System;

	using EyeSoft.Calendar;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class SchedulingRecurrencyDaysTest
	{
		private const int Year = 2013;
		private const int Month = 2;

		private const int EventDay = 6;

		private readonly DateTime eventDate = new DateTime(2012, Month, EventDay);

		private readonly DateTime start = new DateTime(Year, Month, 4);
		private readonly DateTime end = new DateTime(Year, Month, 10);

		[TestMethod]
		public void VerifyThereAreNotDatesWithNoRecurrencyAndEventBeforeTheStartDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.None, start, start.AddDays(5), start.AddDays(-3))
				.Should().Be.Empty();
		}

		[TestMethod]
		public void VerifyThereAreDatesWithDailyRecurrencyAndEventBeforeTheStartDateWithEndDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Daily, start, start.AddDays(7), start.AddDays(-3), start.AddDays(3))
				.Should().Have.SameSequenceAs(start, start.AddDays(1), start.AddDays(2), start.AddDays(3));
		}

		[TestMethod]
		public void VerifyDatesMaintainTimeWithDailyRecurrency()
		{
			var startWithTime = start.AddSeconds(2500);

			Scheduling.RecurrencyDays(RecurrencyFrequency.Daily, startWithTime.Date, startWithTime.AddDays(2).Date, startWithTime)
				.Should().Have.SameSequenceAs(startWithTime, startWithTime.AddDays(1));
		}

		[TestMethod]
		public void VerifyDatesWithDailyRecurrencyWhereEventIsBeforeStartDate()
		{
			var expected =
				new[]
					{
						new DateTime(Year, Month, 4),
						new DateTime(Year, Month, 5),
						new DateTime(Year, Month, EventDay),
						new DateTime(Year, Month, 7),
						new DateTime(Year, Month, 8),
						new DateTime(Year, Month, 9),
						new DateTime(Year, Month, 10)
					};

			Scheduling.RecurrencyDays(RecurrencyFrequency.Daily, start, end, eventDate)
				.Should().Have.SameSequenceAs(expected);
		}

		[TestMethod]
		public void VerifyDatesWithDailyRecurrencyWithEventDatePastStartDate()
		{
			var expected =
				new[]
					{
						new DateTime(Year, Month, 6),
						new DateTime(Year, Month, 7),
						new DateTime(Year, Month, 8),
						new DateTime(Year, Month, 9),
						new DateTime(Year, Month, 10)
					};

			Scheduling.RecurrencyDays(RecurrencyFrequency.Daily, start, end, start.AddDays(2))
				.Should().Have.SameSequenceAs(expected);
		}

		[TestMethod]
		public void VerifyDatesWithWeeklyRecurrencyWhereEventIsBeforeStartDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Weekly, start, end, start.AddDays(-3))
				.Should().Have.SameSequenceAs(new DateTime(Year, Month, 8));
		}

		[TestMethod]
		public void VerifyDatesWithWeeklyRecurrencyWhereEventIsExactlyStartDate()
		{
			var start2 = new DateTime(2014, 11, 10);
			var end2 = new DateTime(2014, 11, 16);
			var currentDate = new DateTime(2014, 09, 15);
			var lastFrequency = new DateTime(2014, 12, 31);

			Scheduling.RecurrencyDays(RecurrencyFrequency.Weekly, start2, end2, currentDate, lastFrequency)
				.Should().Have.SameSequenceAs(start2);
		}

		[TestMethod]
		public void VerifyDatesWithWeeklyRecurrencyWhereEventIsPastStartDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Weekly, start, end, start.AddDays(2))
				.Should().Have.SameSequenceAs(new DateTime(Year, Month, 6));
		}

		[TestMethod]
		public void VerifyDatesWithBiweeklyRecurrencyWhereEventIsBeforeStartDate()
		{
			var endDate = end.AddDays(22);

			Scheduling.RecurrencyDays(RecurrencyFrequency.Biweekly, start, endDate, eventDate)
				.Should().Have.SameSequenceAs(new DateTime(Year, 2, 4), new DateTime(Year, 2, 18), new DateTime(Year, 3, 4));
		}

		[TestMethod]
		public void VerifyDatesWithMontlyRecurrency()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Monthly, start, end, eventDate)
				.Should().Have.SameSequenceAs(new DateTime(Year, Month, EventDay));
		}

		[TestMethod]
		public void VerifyDatesWithYearlyRecurrencyWhereEventIsBeforeStartDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Yearly, start, end, eventDate)
				.Should().Have.SameSequenceAs(new DateTime(Year, Month, EventDay));
		}

		[TestMethod]
		public void VerifyDatesWithYearlyRecurrencyWithEventDatePastStartDate()
		{
			Scheduling.RecurrencyDays(RecurrencyFrequency.Yearly, start, end, eventDate)
				.Should().Have.SameSequenceAs(new DateTime(Year, Month, EventDay));
		}
	}
}