namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class GuidEnsureTest
	{
		[TestMethod]
		public void EnsureThatGuidEmptyIsNotDefault()
		{
			Action action =
				() =>
					Ensure
						.That(Guid.Empty)
						.Is.Not.Default();

			Executing
				.This(action)
				.Should().Throw<NullReferenceException>();
		}
	}
}