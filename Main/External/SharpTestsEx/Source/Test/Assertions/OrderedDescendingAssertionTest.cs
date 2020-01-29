using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class OrderedDescendingAssertionTest
	{
		[Test]
		public void MatchOrdered()
		{
			var oa = new OrderedDescendingAssertion<int>();
			oa.Executing(x => x.Assert(new[] { 3, 2, 1 }, null)).NotThrows();
		}

		[Test]
		public void NotMatchNotOrdered()
		{
			var oa = new OrderedDescendingAssertion<int>();
			oa.Executing(x => x.Assert(new[] { 1, 2, 3 }, null)).Throws<AssertException>();
			oa.Executing(x => x.Assert(new[] { 2, 3, 1 }, null)).Throws<AssertException>();
		}

		[Test]
		public void MessageContainOnlyOriginal()
		{
			var oa = new OrderedDescendingAssertion<int>();
			var fe = oa.Executing(x => x.Assert(new[] { 1, 2, 3 }, "Custom")).Throws<AssertException>().Exception;
			fe.Message.ToLowerInvariant().Should().Contain("be ordered ascending.custom");
		}

		[Test]
		public void MessageShowDifferentPosition()
		{
			var oa = new OrderedDescendingAssertion<int>();
			var fe = oa.Executing(x => x.Assert(new[] { 1, 2, 3 }, "Custom")).Throws<AssertException>().Exception;

			var actualLines = fe.Message.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			actualLines[1].Should().Be.EqualTo("Values differ at position 0.");
			actualLines[2].Should().Be.EqualTo("Expected: 3");
			actualLines[3].Should().Be.EqualTo("Found   : 1");
		}
	}
}