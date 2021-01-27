namespace EyeSoft.Core.Test.Calendar
{
    using Core.Calendar;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class AgnosticDayTest
	{
		[TestMethod]
		public void CreateAValidAgnosticDay()
		{
			var day = new AgnosticDay(24, 4);

			day.Day.Should().Be.EqualTo(24);
			day.Month.Should().Be.EqualTo(4);
		}

		[TestMethod]
		public void CreateANotValidAgnosticDayExpectedException()
		{
			Executing
				.This(() => new AgnosticDay(1, 13))
				.Should().Throw();
		}

		[TestMethod]
		public void CompareEqualsAgnosticDay()
		{
			new AgnosticDay(1, 1)
				.CompareTo(new AgnosticDay(1, 1))
				.Should().Be.EqualTo(0);
		}

		[TestMethod]
		public void CompareMinorAgnosticDay()
		{
			new AgnosticDay(1, 1)
				.CompareTo(new AgnosticDay(2, 1))
				.Should().Be.EqualTo(-1);
		}

		[TestMethod]
		public void CompareMajorAgnosticDay()
		{
			new AgnosticDay(1, 2)
				.CompareTo(new AgnosticDay(2, 1))
				.Should().Be.EqualTo(1);
		}
	}
}