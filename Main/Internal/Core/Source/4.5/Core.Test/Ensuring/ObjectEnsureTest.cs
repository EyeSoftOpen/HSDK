namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ObjectEnsureTest
	{
		[TestMethod]
		public void EnsureThatOnInstanceNotThrow()
		{
			Executing.This(() => Ensure.That(new object()).Is.Not.Null()).Should().NotThrow();
		}

		[TestMethod]
		public void EnsureThatOnNullThrow()
		{
			Executing.This(() => Ensure.That((object)null).Is.Not.Null()).Should().Throw<NullReferenceException>();
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