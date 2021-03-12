namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class DoubleExtensionsTest
	{
		[TestMethod]
		public void VerifyDoubleRoundToIntWorks()
		{
			6.8d.RoundToInt().Should().Be(7);
		}
	}
}