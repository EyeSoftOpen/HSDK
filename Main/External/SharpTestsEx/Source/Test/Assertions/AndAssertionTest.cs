namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class AndAssertionTest
	{
		private class AssertionStub<T> : UnaryAssertion<T>
		{
			private readonly string message;

			public AssertionStub(string message)
				: base("", a => false)
			{
				this.message = message;
			}

			public AssertionStub(string message, bool match)
				: base("", a => match)
			{
				this.message = message;
			}

			public override string GetMessage(T actual, string customMessage)
			{
				return message;
			}
		}

		[Test]
		public void CTor()
		{
			Executing.This(() => new AndAssertion<int>(null, null)).Should().Throw<ArgumentNullException>();
			Executing.This(() => new AndAssertion<int>(new UnaryAssertion<int>("", a => false), null)).Should().Throw<ArgumentNullException>();
			Executing.This(() => new AndAssertion<int>(null, new UnaryAssertion<int>("", a => false))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void Matcher()
		{
			var ass = new AndAssertion<int>(new UnaryAssertion<int>("", a => true), new UnaryAssertion<int>("", a => true));
			Assert.IsTrue(ass.Matcher(1));
			ass = new AndAssertion<int>(new UnaryAssertion<int>("", a => false), new UnaryAssertion<int>("", a => false));
			Assert.IsFalse(ass.Matcher(1));
			ass = new AndAssertion<int>(new UnaryAssertion<int>("", a => true), new UnaryAssertion<int>("", a => false));
			Assert.IsFalse(ass.Matcher(1));
			ass = new AndAssertion<int>(new UnaryAssertion<int>("", a => false), new UnaryAssertion<int>("", a => true));
			Assert.IsFalse(ass.Matcher(1));
		}

		[Test]
		public void GetMessage()
		{
			var ass = new AndAssertion<int>(new AssertionStub<int>("Left message"), new AssertionStub<int>("Right message"));

			var lines = ass.GetMessage(1, null).Split(new[] { Environment.NewLine }, StringSplitOptions.None);

			lines.Should().Have.SameSequenceAs("Left message", "And", "Right message");
		}

		[Test]
		public void WhenOnlyAnOperandFail_ShouldShowOnlyOneMessage()
		{
			var ass = new AndAssertion<int>(new AssertionStub<int>("Left message", true), new AssertionStub<int>("Right message"));
			ass.GetMessage(1, null).Lines().Should().Contain("Right message").And.Not.Contain("Left message").And.Not.Contain("And");

			ass = new AndAssertion<int>(new AssertionStub<int>("Left message"), new AssertionStub<int>("Right message", true));
			ass.GetMessage(1, null).Lines().Should().Contain("Left message").And.Not.Contain("Right message").And.Not.Contain("And");
		}
	}
}