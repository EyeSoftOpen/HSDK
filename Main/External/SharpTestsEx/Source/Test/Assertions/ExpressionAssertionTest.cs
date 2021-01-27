namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class ExpressionAssertionTest
	{
		[Test]
		public void WhenCalledWithNullExpression_Throws()
		{
			Executing.This(() => new ExpressionAssertion<int>(null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void WhenCalledWithNullMessageComposer_Throws()
		{
			Executing.This(() => new ExpressionAssertion<int>(a => a > 0, null)).Should().Throw<ArgumentNullException>();
		}

		[Test]
		public void WhenCalledWithExpression_HasDefaultMessageComposer()
		{
			var exp = new ExpressionAssertion<int>(a => a > 0);
			exp.MessageComposer.Should().Not.Be.Null();
		}

		[Test]
		public void WhenCalledWithExpression_HasTheMatcher()
		{
			var exp = new ExpressionAssertion<int>(a => a > 0);
			exp.Matcher.Should().Not.Be.Null();
		}

		[Test]
		public void TheMatcher_ShouldExecuteTheExpression()
		{
			var myMarker = new object();
			var exp = new ExpressionAssertion<object>(a => ReferenceEquals(a, myMarker));
			exp.Matcher.Invoke(myMarker).Should().Be.True();
			exp.Matcher.Invoke(new object()).Should().Be.False();
		}

		private class MessageComposerMock: IMessageComposer<int>
		{
			#region Implementation of IMessageComposer<int>

			public string GetMessage(int actual, string customMessage)
			{
				GetMessageCalled = true;
				return string.Empty;
			}

			#endregion

			public bool GetMessageCalled { get; private set; }
		}

		[Test]
		public void WhenAssertionFail_CallMessageComposer()
		{
			var composer = new MessageComposerMock();
			var exp = new ExpressionAssertion<int>(a => a > 0, composer);
			exp.Executing(x=> x.Assert(-1, null)).Throws<AssertException>();
			composer.GetMessageCalled.Should().Be.True();
		}
	}
}