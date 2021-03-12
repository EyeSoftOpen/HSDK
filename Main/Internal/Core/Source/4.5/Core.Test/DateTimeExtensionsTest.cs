namespace EyeSoft.Core.Test
{
    using System;
    using System.Threading;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;
    using Threading;

    [TestClass]
	public class DateTimeExtensionsTest
	{
		[TestMethod]
		public void LastDayTest()
		{
			var expected = new DateTime(2012, 7, 29);
			const DayOfWeek DayToCheck = DayOfWeek.Sunday;

			new DateTime(2012, 7, 5)
				.LastDay(DayToCheck)
				.Should().Be(expected);

			new DateTime(2012, 7, 1)
				.LastDay(DayToCheck)
				.Should().Be(expected);

			expected
				.LastDay(DayToCheck)
				.Should().Be(expected);

			new DateTime(2012, 7, 31)
				.LastDay(DayToCheck)
				.Should().Be(expected);
		}

		[TestMethod]
		public void CheckToInvariantCultureDateStringIsCultureIndipendent()
		{
			CheckDateBasedOnDifferenCulture("it");
			CheckDateBasedOnDifferenCulture("en");
		}

		[TestMethod]
		public void CheckFirstDayOfWeekWithSpecifiedCulture()
		{
			Thread.CurrentThread.AssignCulture("it");

			new DateTime(2013, 2, 22).Date.FirstDayOfWeek()
				.Should().Be(new DateTime(2013, 2, 18));
		}

		[TestMethod]
		public void CheckLastDayOfWeekWithSpecifiedCulture()
		{
			Thread.CurrentThread.AssignCulture("it");

			new DateTime(2013, 2, 22).Date.LastDayOfWeek()
				.Should().Be(new DateTime(2013, 2, 24));
		}

		[TestMethod]
		public void VerifyLastDayOfMonth()
		{
			new DateTime(2013, 2, 22).LastDayOfMonth()
				.Should().Be(new DateTime(2013, 2, 28));
		}

		[TestMethod]
		public void VerifyFirstDayOfMonthOfALeapYear()
		{
			new DateTime(2012, 2, 22).FirstDayOfMonth()
				.Should().Be(new DateTime(2012, 2, 1));
		}

		[TestMethod]
		public void VerifyLastDayOfMonthOfALeapYear()
		{
			new DateTime(2012, 2, 22).LastDayOfMonth()
				.Should().Be(new DateTime(2012, 2, 29));
		}

		private static void CheckDateBasedOnDifferenCulture(string lang)
		{
			Thread.CurrentThread.AssignCulture(lang);

			new DateTime(2012, 7, 31)
				.ToInvariantCultureDateString()
				.Should().Be("31072012");
		}
	}
}