namespace EyeSoft.Core.Test
{
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;
    using Text;

    [TestClass]
	public class KnownCharSetTest
	{
		[TestMethod]
		public void KnownLowerCaseCharAreCorrect()
		{
			CharSet
				.LowerCase.All(char.IsLower)
				.Should().BeTrue();
		}

		[TestMethod]
		public void KnownUpperCaseCharAreCorrect()
		{
			CharSet
				.UpperCase.All(char.IsUpper)
				.Should().BeTrue();
		}

		[TestMethod]
		public void KnownDigitCharAreCorrect()
		{
			CharSet
				.Numbers.All(char.IsNumber)
				.Should().BeTrue();
		}
	}
}