namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class IntegerEnsureTest
	{
		[TestMethod]
		public void VerifyEnsureGreaterThanZeroWorksOnLong()
		{
			Action ensure = () =>
				Ensure
					.That(-1L)
					.Is.GreaterThanZero();

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void VerifyEnsureGreaterThanWorksOnLong()
		{
			Action ensure = () =>
				Ensure
					.That(0L)
					.Is.GreaterThan(1);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}
	}
}