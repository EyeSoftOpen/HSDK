namespace EyeSoft.Core.Test.Calendar
{
    using System;
    using EyeSoft.Calendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class SchedulingStartRecurrencyDateTest
	{
		private const int Year = 2013;
		private const int Month = 2;

		private readonly DateTime start = new DateTime(Year, Month, 4);

		[TestMethod]
		public void VerifyNextDateOfANoRecurrencyEventIsTheSameDateOfTheEvent()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.None, start, start)
				.Should().Be(start);
		}

		[TestMethod]
		public void VerifyNextDateOfANoRecurrencyExpiredEventIsNull()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.None, start.AddDays(10), start)
				.Should().NotHaveValue();
		}

		[TestMethod]
		public void VerifyDatesWithDailyRecurrencyWhereEventIsBeforeStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Weekly, start, new DateTime(Year, Month, 6))
				.Should().Be(new DateTime(Year, Month, 6));
		}

		[TestMethod]
		public void VerifyDatesWithDailyRecurrencyWhereEventIsPastStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Weekly, start, new DateTime(Year, Month, 7))
				.Should().Be(new DateTime(Year, Month, 7));
		}

		[TestMethod]
		public void VerifyStartBiweeklyRecurrencyDateMaintainTimeOfTheEvent()
		{
			const int SecondsToAdd = 900;

			var eventDateWithTime = start.AddDays(-7).AddSeconds(SecondsToAdd);

			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Biweekly, start, eventDateWithTime)
				.Should().Be(eventDateWithTime.AddDays(14));
		}

		[TestMethod]
		public void VerifyDatesWithMonthlyRecurrencyWhereEventIsBeforeStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Monthly, new DateTime(2012, 12, 31), new DateTime(2012, 12, 15))
				.Should().Be(new DateTime(2013, 1, 15));
		}

		[TestMethod]
		public void VerifyDatesWithMonthlyRecurrencyWhereEventIsPastStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Monthly, new DateTime(2012, 12, 31), new DateTime(2013, 1, 15))
				.Should().Be(new DateTime(2013, 1, 15));
		}

		[TestMethod]
		public void VerifyDatesWithYearlyRecurrencyWhereEventIsBeforeStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Monthly, new DateTime(2012, 12, 31), new DateTime(2012, 12, 15))
				.Should().Be(new DateTime(2013, 1, 15));
		}

		[TestMethod]
		public void VerifyDatesWithYearlyRecurrencyWhereEventIsPastStartDate()
		{
			Scheduling.StartRecurrencyDate(RecurrencyFrequency.Yearly, new DateTime(2012, 12, 31), new DateTime(2013, 1, 15))
				.Should().Be(new DateTime(2013, 1, 15));
		}
	}
}