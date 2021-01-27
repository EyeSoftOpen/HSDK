namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class NegateAssertionTest
	{
		private class BaseAssertionStub<T>: UnaryAssertion<T>
		{
			public BaseAssertionStub(string predicate, Func<T, bool> matcher) : base(predicate, matcher) {}
			public override string GetMessage(T actual, string customMessage)
			{
				return "My Message";
			}
		}

		[Test]
		public void CTor()
		{
			Executing.This(() => new NegateAssertion<int>(null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void MatchNegation()
		{
			var na = new NegateAssertion<int>(new UnaryAssertion<int>("Predicate", a => true));
			Assert.IsFalse(na.Matcher(1));
		}

		[Test]
		public void MessageNegation()
		{
			var na = new NegateAssertion<int>(new BaseAssertionStub<int>("Predicate", a => true));
			Assert.AreEqual("My Message", na.GetMessage(0, null));
			Assert.AreNotEqual("Predicate", na.Predicate);
			StringAssert.Contains("Not", na.Predicate);
		}
	}
}