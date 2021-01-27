namespace EyeSoft.Core.Test
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class ComparerTest
	{
		[TestMethod]
		public void VerifyMinBetweenDateTime()
		{
			var minDate = new DateTime(2013, 5, 7, 2, 2, 0);
			var maxDate = minDate.AddDays(3);

			Comparer.Min(minDate, maxDate).Should().Be.EqualTo(minDate);
		}

		[TestMethod]
		public void VerifyMaxBetweenDateTime()
		{
			var minDate = new DateTime(2013, 5, 7, 2, 2, 0);
			var maxDate = minDate.AddDays(3);

			Comparer.Max(minDate, maxDate).Should().Be.EqualTo(maxDate);
		}

		[TestMethod]
		public void VerifyEqualsBetweenDifferentDateTimeIsFalse()
		{
			var minDate = new DateTime(2013, 5, 7, 2, 2, 0);
			var maxDate = minDate.AddDays(3);

			Comparer.Equals(minDate, maxDate).Should().Be.False();
		}

		[TestMethod]
		public void VerifyEqualsBetweenTheSameDateTimeIsTrue()
		{
			var minDate = new DateTime(2013, 5, 7, 2, 2, 0);
			var maxDate = minDate;

			Comparer.Equals(minDate, maxDate).Should().Be.True();
		}
	}
}