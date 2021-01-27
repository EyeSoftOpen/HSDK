namespace SharpTestsEx.Test.Assertions
{
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class DefaultMessageBuilderTest
	{
		[Test]
		public void ComposeInt()
		{
			var mb = new DefaultMessageBuilder<int, int>();
			var expected = "1 Should Be Equal to 2.User Message";
			var actual = mb.Compose(new MessageBuilderInfo<int, int> { Actual=1,Expected=2,AssertionPredicate = "Be Equal to", CustomMessage = "User Message" });
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ComposeString()
		{
			var mb = new DefaultMessageBuilder<string, string>();
			var expected = "\"A\" Should Be Equal to \"B\".User Message";
			var actual = mb.Compose(new MessageBuilderInfo<string, string> { Actual = "A", Expected = "B", AssertionPredicate = "Be Equal to", CustomMessage = "User Message" });
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void ComposeNull()
		{
			var mb = new DefaultMessageBuilder<string, string>();
			var expected = "(null) Should Be Equal to \"B\".User Message";
			var actual = mb.Compose(new MessageBuilderInfo<string, string> { Actual = null, Expected = "B", AssertionPredicate = "Be Equal to", CustomMessage = "User Message" });
			Assert.AreEqual(expected, actual);
		}
	}
}