namespace EyeSoft.Core.Test.Calendar
{
    using System;
    using EyeSoft.Calendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class SchedulingCalendarRangeTest
	{
		private const int Year = 2019;
		private const int Month = 3;

		[TestMethod]
		public void VerifyDaysInRangeFromDateAndVistaSevenAreCorrect()
		{
			var agendaDayWithTasks = Scheduling.Range(new DateTime(Year, Month, 21), CalendarView.Week);

			agendaDayWithTasks.First.Date.Should().Be.EqualTo(new DateTime(Year, Month, 18));
			agendaDayWithTasks.Last.Date.Should().Be.EqualTo(new DateTime(Year, Month, 24));
		}

		[TestMethod]
		public void VerifyDaysInRangeFromDateAndVistaMonthAreCorrect()
		{
			var agendaDayWithTasks = Scheduling.Range(new DateTime(Year, Month, 21), CalendarView.Month);

			agendaDayWithTasks.First.Date.Should().Be.EqualTo(new DateTime(Year, 2, 25));
			agendaDayWithTasks.Last.Date.Should().Be.EqualTo(new DateTime(Year, 4, 6));
		}
	}
}
