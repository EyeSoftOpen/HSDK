namespace EyeSoft.Core.Test
{
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class BooleanExtensionsTest
	{
		[TestMethod]
		public void OnTrueVerifyActionIsExecuted()
		{
			Executing(true, true);
		}

		[TestMethod]
		public void OnFalseVerifyActionIsNotExecuted()
		{
			Executing(false, false);
		}

		private static void Executing(bool value, bool expectedExecuted)
		{
			var executed = false;
			value.Extend().OnTrueExecute(() => executed = true);

			executed.Should().Be(expectedExecuted);
		}
	}
}