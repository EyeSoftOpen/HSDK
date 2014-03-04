namespace EyeSoft.Test.Calendar
{
	using System;

	using EyeSoft.Calendar;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class HolidayFactoryTest
	{
		private const string FirstDayOfYear = "First day of year";
		private const string Epiphany = "Epiphany";
		private const string Heaster = "Heaster";

		private const string EasterMonday = "Easter Monday";

		[TestMethod]
		public void AddAHolidayThatIsMoreRecentOfAnyExpectedWorks()
		{
			var expected =
				new[]
					{
						Holiday.Fixed(FirstDayOfYear, new DateTime(2011, 1, 1)),
						Holiday.Fixed(Epiphany, new DateTime(2011, 1, 6)),
						Holiday.Fixed(FirstDayOfYear, new DateTime(2012, 1, 1)),
						Holiday.Fixed(Epiphany, new DateTime(2012, 1, 6)),
						Holiday.Fixed(FirstDayOfYear, new DateTime(2013, 1, 1)),
						Holiday.Fixed(Epiphany, new DateTime(2013, 1, 6))
					};

			new HolidayFactory(2011, 2)
				.Fixed(FirstDayOfYear, new AgnosticDay(1, 1))
				.Fixed(Epiphany, new AgnosticDay(6, 1))
				.List()
				.Should().Have.SameSequenceAs(expected);
		}

		[TestMethod]
		public void AddAHolidayThatIsLessRecentOfAnyExpectedException()
		{
			Action action =
				() =>
					new HolidayFactory(2011, 2)
						.Fixed(Epiphany, new AgnosticDay(1, 6))
						.Fixed(FirstDayOfYear, new AgnosticDay(1, 1))
						.List();

			Executing
				.This(action)
				.Should().Throw<ArgumentException>();
		}

		[TestMethod]
		public void AddHolidaysForOneYearVerifyAreCorrect()
		{
			var expected =
				new[]
					{
						Holiday.Fixed(FirstDayOfYear, new DateTime(2011, 1, 1)),
						Holiday.Fixed(Epiphany, new DateTime(2011, 1, 6)),
						Holiday.Entry(Heaster, new DateTime(2011, 4, 24)),
						Holiday.Entry(EasterMonday, new DateTime(2011, 4, 25))
					};

			new HolidayFactory(2011)
				.Fixed(FirstDayOfYear, new AgnosticDay(1, 1))
				.Fixed(Epiphany, new AgnosticDay(6, 1))
				.Entry(Heaster, new AgnosticDay(24, 4))
					.NextDay(EasterMonday)
				.List()
				.Should().Have.SameSequenceAs(expected);
		}
	}
}