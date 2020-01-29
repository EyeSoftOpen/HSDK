using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;
using SharpTestsEx.ExtensionsImpl;

namespace SharpTestsEx.Tests.ExtensionsImpl
{
	
	public class ConstraintTest
	{
		private class ConstraintStub<T> : Constraint<T>
		{
			public ConstraintStub(T actual) : base(actual) { }
			public ConstraintStub(T actual, Func<string> messageBuilder) : base(actual, messageBuilder) { }
		}

		[Test]
		public void CTor()
		{
			var c = new ConstraintStub<int>(5);
			Assert.IsNotNull(c.AssertionInfo);
			Assert.AreEqual(5, c.AssertionInfo.Actual);
			Assert.IsNull(c.Parent);
		}

		[Test]
		public void AssertUsingShouldWorkWithActual()
		{
			var c = new ConstraintStub<int>(5);
			c.AssertionInfo.AssertUsing(new Assertion<int, int>("", 5, a => a == 5));
			Executing.This(() => c.AssertionInfo.AssertUsing(new Assertion<int, int>("", 0, a => false))).Should().Throw<AssertException>();
		}

		[Test]
		public void AssertUsingShouldUseCustomMessage()
		{
			var assertionTitle = "An integer can't be null";
			var cm = new ConstraintStub<int>(5, () => assertionTitle);
			try
			{
				cm.AssertionInfo.AssertUsing(new Assertion<int, int>("", 0, a => false));
			}
			catch (AssertException ae)
			{
				StringAssert.Contains(assertionTitle, ae.Message);
			}
		}
	}
}