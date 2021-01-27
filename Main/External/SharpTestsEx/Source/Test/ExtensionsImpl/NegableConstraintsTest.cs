namespace SharpTestsEx.Test.ExtensionsImpl
{
    using NUnit.Framework;
    using SharpTestsEx.ExtensionsImpl;

    public class NegableConstraintsTest
	{
		private class ConstraintStub<T> : Constraint<T>
		{
			public ConstraintStub(T actual) : base(actual) { }
		}

		private class NegableConstraintsStub<T, TConstraints> : NegableConstraints<T, TConstraints> where TConstraints:class
		{
			public NegableConstraintsStub(T actual) : base(actual) {}
		}

		[Test]
		public void AssertUsingShouldWorkWithActual()
		{
			var c = new NegableConstraintsStub<int, ConstraintStub<int>>(4);
			var p = c.Not;
			Assert.IsTrue(c.AssertionInfo.IsNegated);
		}
	}
}