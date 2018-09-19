namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class DoubleEnsureTest
	{
		[TestMethod]
		public void EnsureGreaterThanZeroOnDoubleThrowExceptionOnNegative()
		{
			Action ensure = () =>
				Ensure
					.That(-1d)
					.Is.GreaterThanZero();

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureGreaterThanZeroOnDoubleWorksOnPositive()
		{
			Ensure
				.That(1d)
				.Is.GreaterThanZero();
		}

		[TestMethod]
		public void EnsureGreaterThanOnDoubleThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(0d)
					.Is.GreaterThan(1d);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnMinorValueOnDoubleThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(5d)
					.Is.Between(6d, 10d);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnMajorValueOnDoubleThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(11d)
					.Is.Between(6d, 10d);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnValueValueOnDoubleThrowException()
		{
			Ensure
				.That(6d)
				.Is.Between(6d, 10d);
		}
	}
}