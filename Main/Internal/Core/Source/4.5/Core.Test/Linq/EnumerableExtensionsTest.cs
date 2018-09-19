namespace EyeSoft.Test.Linq
{
	using System.Linq;

	using EyeSoft.Collections.Generic;
	using EyeSoft.Test.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class EnumerableExtensionsTest
	{
		[TestMethod]
		public void VerifySinglePropertyFunction()
		{
			KnownEntity
				.List
				.Where(KnownExpressions.SingleProperty.Compiled)
				.Count()
				.Should()
				.Be
				.EqualTo(1);
		}
	}
}