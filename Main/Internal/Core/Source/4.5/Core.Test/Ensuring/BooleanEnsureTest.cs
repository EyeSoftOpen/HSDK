namespace EyeSoft.Test.Ensuring
{
	using System;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class BooleanEnsureTest
	{
		[TestMethod]
		public void EnsureTrueWorks()
		{
			Ensure
				.That(true)
				.Is.True();
		}

		[TestMethod]
		public void EnsureTrueThrowExceptionOnFalse()
		{
			Action ensure = () =>
				Ensure
					.That(false)
					.Is.True();

			this
				.Executing(a => ensure())
				.Throws<ConditionNotVerifiedException>();
		}
	}
}