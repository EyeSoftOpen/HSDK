namespace SharpTestsEx.Test.Assertions
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class OrderedAscendingAssertionTest
	{
		[Test]
		public void MatchOrdered()
		{
			var oa = new OrderedAscendingAssertion<int>();
			oa.Executing(x => x.Assert(new[] {1, 2, 3}, null)).NotThrows();
		}

		[Test]
		public void NotMatchNotOrdered()
		{
			var oa = new OrderedAscendingAssertion<int>();
			oa.Executing(x => x.Assert(new[] { 3, 2, 1 }, null)).Throws<AssertException>();
			oa.Executing(x => x.Assert(new[] { 1, 3, 2 }, null)).Throws<AssertException>();
		}

		[Test]
		public void MessageContainOnlyOriginal()
		{
			var oa = new OrderedAscendingAssertion<int>();
			oa.Executing(x => x.Assert(new[] { 3, 2, 1 }, "Custom")).Throws<AssertException>()
			.And.ValueOf.Message.ToLowerInvariant().Should().Contain("be ordered ascending.custom");
		}

		[Test]
		public void MessageShowDifferentPosition()
		{
			var oa = new OrderedAscendingAssertion<int>();
			var fe = oa.Executing(x => x.Assert(new[] { 3, 2, 1 }, "Custom")).Throws<AssertException>().Exception;

			var actualLines = fe.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			actualLines[1].Should().Be.EqualTo("Values differ at position 0.");
			actualLines[2].Should().Be.EqualTo("Expected: 1");
			actualLines[3].Should().Be.EqualTo("Found   : 3");
		}
	}
}