namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class UnaryAssertionTest
	{
		[Test]
		public void WrongCtor()
		{
			Executing.This(() => new UnaryAssertion<int>(string.Empty, null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void CtorWithNullPredicateIgnore()
		{
			var ba = new UnaryAssertion<int>(null, a => true);
			Assert.AreEqual(string.Empty, ba.Predicate);
		}

		[Test]
		public void Matcher()
		{
			var ba = new UnaryAssertion<int>(null, a => true);
			Assert.IsTrue(ba.Matcher(1));
			ba = new UnaryAssertion<int>(null, a => false);
			Assert.IsFalse(ba.Matcher(1));
		}

		[Test]
		public void AssertFail()
		{
			var ba = new UnaryAssertion<int>(null, a => false);
			ba.Executing(x => x.Assert(1, null)).Throws<AssertException>();
		}

		[Test]
		public void AssertDoesNotFail()
		{
			var ba = new UnaryAssertion<int>(null, a => true);
			ba.Assert(1, null);
		}
	}
}