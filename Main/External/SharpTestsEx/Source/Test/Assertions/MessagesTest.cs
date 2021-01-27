namespace SharpTestsEx.Test.Assertions
{
    using NUnit.Framework;
    using SharpTestsEx.Assertions;

    public class MessagesTest
	{
		[Test]
		public void FormatEnumerable()
		{
			Assert.AreEqual("[1, 2, 3]", Messages.FormatEnumerable(new[] { 1, 2, 3 }));
			Assert.AreEqual("[1, (null), 1]", Messages.FormatEnumerable(new object[] { 1, null, 1 }));
			Assert.AreEqual("[\"A\", (null), \"B\"]", Messages.FormatEnumerable(new[] { "A", null, "B" }));
			Assert.AreEqual("[<Empty>]", Messages.FormatEnumerable(new object[0]));
		}
	}
}