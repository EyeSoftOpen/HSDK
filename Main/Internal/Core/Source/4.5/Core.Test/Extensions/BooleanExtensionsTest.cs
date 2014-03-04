namespace EyeSoft.Test
{
	using EyeSoft.Extensions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

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

			executed.Should().Be.EqualTo(expectedExecuted);
		}
	}
}