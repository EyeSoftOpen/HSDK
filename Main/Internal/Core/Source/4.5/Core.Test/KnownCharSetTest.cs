namespace EyeSoft.Test
{
	using System.Linq;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class KnownCharSetTest
	{
		[TestMethod]
		public void KnownLowerCaseCharAreCorrect()
		{
			CharSet
				.LowerCase.All(char.IsLower)
				.Should().Be.True();
		}

		[TestMethod]
		public void KnownUpperCaseCharAreCorrect()
		{
			CharSet
				.UpperCase.All(char.IsUpper)
				.Should().Be.True();
		}

		[TestMethod]
		public void KnownDigitCharAreCorrect()
		{
			CharSet
				.Numbers.All(char.IsNumber)
				.Should().Be.True();
		}
	}
}