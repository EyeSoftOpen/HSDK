namespace SharpTestsEx.Test.Assertions
{
    using System.Collections.Generic;
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class EnumerableMessageBuilderTest
	{
		[Test]
		public void ComposeNull()
		{
			var mb = new EnumerableMessageBuilder<int>();
			string expected = "(null) Should Have Same sequence as [1, 2, 3].User Message";
			string actual =
				mb.Compose(new MessageBuilderInfo<IEnumerable<int>, IEnumerable<int>>
				           	{
				           		Actual = null,
				           		Expected = new[] {1, 2, 3},
				           		AssertionPredicate = "Have Same sequence as",
				           		CustomMessage = "User Message"
				           	});
			Assert.AreEqual(expected, actual);

			expected = "[1, 2, 3] Should Have Same sequence as (null).User Message";
			actual =
				mb.Compose(new MessageBuilderInfo<IEnumerable<int>, IEnumerable<int>>
				           	{
				           		Actual = new[] {1, 2, 3},
				           		Expected = null,
				           		AssertionPredicate = "Have Same sequence as",
				           		CustomMessage = "User Message"
				           	});
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ComposeWithDifferences()
		{
			var mb = new EnumerableMessageBuilder<int>((a, e) => new[] {1, 3});
			var info = new MessageBuilderInfo<IEnumerable<int>, IEnumerable<int>> { Actual = new[] { 1, 1 }, Expected = new[] { 1, 1 } };
			mb.Compose(info).Should().Contain("Differences :[1, 3]");
		}
	}
}