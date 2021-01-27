namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class OrAssertionTest
	{
		private class AssertionStub<T> : UnaryAssertion<T>
		{
			private readonly string message;

			public AssertionStub(string message)
				: base("", a => false)
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
			Executing.This(() => new OrAssertion<int>(null, null)).Should().Throw<ArgumentNullException>();
			Executing.This(() => new OrAssertion<int>(new UnaryAssertion<int>("", a => false), null)).Should().Throw<ArgumentNullException>();
			Executing.This(() => new OrAssertion<int>(null, new UnaryAssertion<int>("", a => false))).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void Matcher()
		{
			var ass = new OrAssertion<int>(new UnaryAssertion<int>("", a => true), new UnaryAssertion<int>("", a => true));
			Assert.IsTrue(ass.Matcher(1));
			ass = new OrAssertion<int>(new UnaryAssertion<int>("", a => false), new UnaryAssertion<int>("", a => false));
			Assert.IsFalse(ass.Matcher(1));
			ass = new OrAssertion<int>(new UnaryAssertion<int>("", a => true), new UnaryAssertion<int>("", a => false));
			Assert.IsTrue(ass.Matcher(1));
			ass = new OrAssertion<int>(new UnaryAssertion<int>("", a => false), new UnaryAssertion<int>("", a => true));
			Assert.IsTrue(ass.Matcher(1));
		}

		[Test]
		public void GetMessage()
		{
			var ass = new OrAssertion<int>(new AssertionStub<int>("Left message"), new AssertionStub<int>("Right message"));

			var lines = ass.GetMessage(1, null).Split(new[] {Environment.NewLine}, StringSplitOptions.None);

			lines.Should().Have.SameSequenceAs("Left message", "Or", "Right message");
		}
	}
}