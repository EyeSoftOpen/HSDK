namespace SharpTestsEx.Test.ExtensionsImpl
{
    using NUnit.Framework;
    using SharpTestsEx.ExtensionsImpl;

    public class ChildConstraintTest
	{
		private class ConstraintStub<T> : Constraint<T>
		{
			public ConstraintStub(T actual) : base(actual) { }
		}

		private class ChildConstraintStub<T> : ChildConstraint<T, IConstraints<T>>
		{
			public ChildConstraintStub(IConstraints<T> parent) : base(parent) { }
		}

		[Test]
		public void CTor()
		{
			var c = new ConstraintStub<int>(5);
			var cc = new ChildConstraintStub<int>(c);

			Assert.IsNotNull(cc.AssertionInfo);
			Assert.AreEqual(5, cc.AssertionInfo.Actual);
			Assert.AreSame(c, cc.AssertionParent);
		}
	}
}