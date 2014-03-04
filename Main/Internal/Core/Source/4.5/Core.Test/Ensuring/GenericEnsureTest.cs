namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class GenericEnsureTest
	{
		[TestMethod]
		public void GenericEqualsToWork()
		{
			const string TestValue = "abc";

			Ensure
				.That(TestValue)
				.Is.EqualsTo(TestValue);
		}

		[TestMethod]
		public void GenericEqualsToThrowExceptionOnNoteEqualsValues()
		{
			const string TestValue = "abc";

			Action ensure = () =>
				Ensure
					.That(TestValue)
					.Is.EqualsTo(string.Empty);

			this
				.Executing(a => ensure())
				.Throws<NotEqualException>();
		}
	}
}