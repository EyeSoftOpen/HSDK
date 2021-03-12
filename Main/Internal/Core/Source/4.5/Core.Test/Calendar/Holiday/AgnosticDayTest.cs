namespace EyeSoft.Core.Test.Calendar
{
    using System;
    using EyeSoft.Calendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class AgnosticDayTest
	{
		[TestMethod]
		public void CreateAValidAgnosticDay()
		{
			var day = new AgnosticDay(24, 4);

			day.Day.Should().Be(24);
			day.Month.Should().Be(4);
		}

		[TestMethod]
		public void CreateANotValidAgnosticDayExpectedException()
		{
			Action action = () => new AgnosticDay(1, 13);

            action.Should().Throw<Exception>();
		}

		[TestMethod]
		public void CompareEqualsAgnosticDay()
		{
			new AgnosticDay(1, 1)
				.CompareTo(new AgnosticDay(1, 1))
				.Should().Be(0);
		}

		[TestMethod]
		public void CompareMinorAgnosticDay()
		{
			new AgnosticDay(1, 1)
				.CompareTo(new AgnosticDay(2, 1))
				.Should().Be(-1);
		}

		[TestMethod]
		public void CompareMajorAgnosticDay()
		{
			new AgnosticDay(1, 2)
				.CompareTo(new AgnosticDay(2, 1))
				.Should().Be(1);
		}
	}
}