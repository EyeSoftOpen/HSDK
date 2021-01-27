namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class DoubleExtensionsTest
	{
		[TestMethod]
		public void VerifyDoubleRoundToIntWorks()
		{
			6.8d.RoundToInt().Should().Be.EqualTo(7);
		}
	}
}