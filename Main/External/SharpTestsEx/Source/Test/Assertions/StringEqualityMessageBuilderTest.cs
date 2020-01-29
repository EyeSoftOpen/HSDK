using System;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class StringEqualityMessageBuilderTest
	{
		[Test]
		public void ComposeEqualsStrings()
		{
			var mb = new StringEqualityMessageBuilder();
			mb.Executing(x => x.Compose(new MessageBuilderInfo<string, string> {Actual = "B", Expected = "B"})).NotThrows();
		}

		[Test]
		public void ComposeNull()
		{
			var mb = new StringEqualityMessageBuilder();
			string expected = "(null) Should Be Equal to \"B\".User Message";
			string actual =
				mb.Compose(new MessageBuilderInfo<string, string>
				           	{Actual = null, Expected = "B", AssertionPredicate = "Be Equal to", CustomMessage = "User Message"});
			Assert.AreEqual(expected, actual);

			expected = "\"A\" Should Be Equal to (null).User Message";
			actual =
				mb.Compose(new MessageBuilderInfo<string, string>
				           	{Actual = "A", Expected = null, AssertionPredicate = "Be Equal to", CustomMessage = "User Message"});
			Assert.AreEqual(expected, actual);
		}

		[Test]
		public void DiffStringMsg()
		{
			var mb = new StringEqualityMessageBuilder();
			var info = new MessageBuilderInfo<string, string>
			           	{
			           		Actual = "something",
			           		Expected = "someXhing",
			           		AssertionPredicate = "Be Equal to",
			           		CustomMessage = "User Message"
			           	};
			string actual = mb.Compose(info);
			// contains default message
			StringAssert.Contains("Should Be Equal to", actual);
			StringAssert.Contains("\"" + info.Actual + "\"", actual);
			StringAssert.Contains("\"" + info.Expected + "\"", actual);
			StringAssert.Contains(info.CustomMessage, actual);

			string[] actualLines = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			// contains enhanced message
			Assert.AreEqual(actualLines[1], "Strings differ at position 5.");
			Assert.AreEqual(actualLines[2], "something");
			Assert.AreEqual(actualLines[3], "someXhing");
			Assert.AreEqual(actualLines[4], "____^____");
		}

		[Test]
		public void DiffLongStringMsg()
		{
			// For strings with lenth greater than 20 the message should cut equal chars
			// and show the difference between 10 chars
			var mb = new StringEqualityMessageBuilder();
			var info = new MessageBuilderInfo<string, string>
			           	{
										Actual = "1234567890123456789012345678901234567890something",
										Expected = "1234567890123456789012345678901234567890someXhing",
			           		AssertionPredicate = "Be Equal to",
			           		CustomMessage = "User Message"
			           	};
			string actual = mb.Compose(info);
			// contains default message
			StringAssert.Contains("Should Be Equal to", actual);
			StringAssert.Contains("\"" + info.Actual + "\"", actual);
			StringAssert.Contains("\"" + info.Expected + "\"", actual);
			StringAssert.Contains(info.CustomMessage, actual);

			string[] actualLines = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			// contains enhanced message
			Assert.AreEqual(actualLines[1], "Strings differ at position 45.");
			Assert.AreEqual(actualLines[2], "...0123456789012345678901234567890something");
			Assert.AreEqual(actualLines[3], "...0123456789012345678901234567890someXhing");
			Assert.AreEqual(actualLines[4], "...___________________________________^____");
		}

		[Test]
		public void DiffLongEndStringMsg()
		{
			var mb = new StringEqualityMessageBuilder();
			var info = new MessageBuilderInfo<string, string>
			           	{
										Actual = "something1234567890123456789012345678901234567890",
										Expected = "someXhing1234567890123456789012345678901234567890",
			           		AssertionPredicate = "Be Equal to",
			           		CustomMessage = "User Message"
			           	};
			string actual = mb.Compose(info);
			// contains default message
			StringAssert.Contains("Should Be Equal to", actual);
			StringAssert.Contains("\"" + info.Actual + "\"", actual);
			StringAssert.Contains("\"" + info.Expected + "\"", actual);
			StringAssert.Contains(info.CustomMessage, actual);

			string[] actualLines = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			// contains enhanced message
			Assert.AreEqual(actualLines[1], "Strings differ at position 5.");
			Assert.AreEqual(actualLines[2], "something1234567890123456789012345678901...");
			Assert.AreEqual(actualLines[3], "someXhing1234567890123456789012345678901...");
			Assert.AreEqual(actualLines[4], "____^___________________________________...");
		}

		[Test]
		public void DiffLongStartLongEndString()
		{
			var mb = new StringEqualityMessageBuilder();
			var info = new MessageBuilderInfo<string, string>
			           	{
										Actual = "1234567890123456789012345678901234567890something1234567890123456789012345678901234567890",
										Expected = "1234567890123456789012345678901234567890someXhing1234567890123456789012345678901234567890",
			           		AssertionPredicate = "Be Equal to",
			           		CustomMessage = "User Message"
			           	};
			string actual = mb.Compose(info);
			// contains default message
			StringAssert.Contains("Should Be Equal to", actual);
			StringAssert.Contains("\"" + info.Actual.Substring(0,80) + "...\"", actual);
			StringAssert.Contains("\"" + info.Expected.Substring(0, 80) + "...\"", actual);
			StringAssert.Contains(info.CustomMessage, actual);

			string[] actualLines = actual.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			// contains enhanced message
			Assert.AreEqual(actualLines[1], "Strings differ at position 45.");
			Assert.AreEqual(actualLines[2], "...5678901234567890something123456789012345...");
			Assert.AreEqual(actualLines[3], "...5678901234567890someXhing123456789012345...");
			Assert.AreEqual(actualLines[4], "...____________________^___________________...");
		}

		[Test]
		public void StringConstraintShouldUseStringEqualityMessageBuilder()
		{
			var e = Executing.This(() => "something".Should().Be.EqualTo("someXhing")).Should().Throw<AssertException>().Exception;
			StringAssert.Contains("____^____", e.Message);
		}

		[Test]
		public void FormatValue()
		{
			Assert.AreEqual(StringEqualityMessageBuilder.FormatValue(null), "(null)");
			Assert.AreEqual(StringEqualityMessageBuilder.FormatValue("AA"), "\"AA\"");
			Assert.AreEqual(StringEqualityMessageBuilder.FormatValue("123456789012345678901234567890123456789012345678901234567890someXhing12345678901234567890"), "\"123456789012345678901234567890123456789012345678901234567890someXhing12345678901...\"");
		}
	}
}