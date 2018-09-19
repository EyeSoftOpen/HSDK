namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class LongEnsureTest
	{
		[TestMethod]
		public void EnsureGreaterThanZeroOnInt32ThrowExceptionOnNegative()
		{
			Action ensure = () =>
				Ensure
					.That(-1)
					.Is.GreaterThanZero();

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureGreaterThanZeroOnInt32WorksOnPositive()
		{
			Ensure
				.That(1)
				.Is.GreaterThanZero();
		}

		[TestMethod]
		public void EnsureGreaterThanOnInt32ThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(0)
					.Is.GreaterThan(1);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnMinorValueOnInt32ThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(5)
					.Is.Between(6, 10);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnMajorValueOnInt32ThrowException()
		{
			Action ensure = () =>
				Ensure
					.That(11)
					.Is.Between(6, 10);

			this
				.Executing(a => ensure())
				.Throws<ArgumentOutOfRangeException>();
		}

		[TestMethod]
		public void EnsureBetweenOnValueValueOnInt32ThrowException()
		{
			Ensure
				.That(6)
				.Is.Between(6, 10);
		}
	}
}