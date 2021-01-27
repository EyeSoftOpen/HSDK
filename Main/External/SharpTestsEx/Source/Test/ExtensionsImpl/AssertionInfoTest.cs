namespace SharpTestsEx.Test.ExtensionsImpl
{
    using NUnit.Framework;
    using SharpTestsEx.ExtensionsImpl;

    public class AssertionInfoTest
	{
		[Test]
		public void NegationShouldWorkFine()
		{
			var ai = new AssertionInfo<int>(5);
			var nai = (INegable) ai;
			nai.Nagate();
			Assert.IsTrue(ai.IsNegated);
			nai.Nagate();
			Assert.IsFalse(ai.IsNegated);
			nai.Nagate();
			Assert.IsTrue(ai.IsNegated);
		}

		[Test]
		public void ShouldAcceptNullAsActual()
		{
			var ai = new AssertionInfo<object>(null);
			Assert.IsNull(ai.Actual);
		}

		[Test]
		public void MessageProviderShouldWork()
		{
			const string fixedMessage = "My message";
			var ai = new AssertionInfo<object>(null, () => fixedMessage);
			Assert.AreEqual(fixedMessage, ai.FailureMessage);

			ai = new AssertionInfo<object>(null, () => string.Format("M{0}{1}{2}", 1, 2, 3));
			Assert.AreEqual("M123", ai.FailureMessage);

			ai = new AssertionInfo<object>(null);
			Assert.IsNull(ai.FailureMessage);
		}
	}
}