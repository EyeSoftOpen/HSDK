using System;
using System.Collections.Generic;
using NUnit.Framework;
using SharpTestsEx.Assertions;

namespace SharpTestsEx.Tests.Assertions
{
	
	public class SameSequenceAsMessageBuilderTest
	{
		[Test]
		public void ShouldContainDiffPositionAndValues()
		{
			var mb = new SameSequenceAsMessageBuilder<int>();
			var info = new MessageBuilderInfo<IEnumerable<int>, IEnumerable<int>>
			           	{
			           		Actual = new[] {1, 2, 3},
			           		Expected = new[] {1, 3, 3},
			           	};

			var actualLines = mb.Compose(info).Split(new[] {Environment.NewLine}, StringSplitOptions.None);
			Assert.AreEqual(actualLines[1], "Values differ at position 1 (zero based).");
			Assert.AreEqual(actualLines[2], "Expected: 3");
			Assert.AreEqual(actualLines[3], "Found   : 2");
		}

		[Test]
		public void ShouldContainDiffPositionAndValuesNulls()
		{
			var mb = new SameSequenceAsMessageBuilder<string>();
			var info = new MessageBuilderInfo<IEnumerable<string>, IEnumerable<string>>
			{
				Actual = new[] { "A", null, "b" },
				Expected = new[] { null, null, "b" },
			};

			var actualLines = mb.Compose(info).Split(new[] { Environment.NewLine }, StringSplitOptions.None);
			Assert.AreEqual("Values differ at position 0.", actualLines[1]);
			Assert.AreEqual(actualLines[2], "Expected: (null)");
			Assert.AreEqual(actualLines[3], "Found   : \"A\"");
		}

		[Test]
		public void ShouldDoesNotFailForNullComparison()
		{
			var mb = new SameSequenceAsMessageBuilder<int>();
			var info = new MessageBuilderInfo<IEnumerable<int>, IEnumerable<int>>
			{
				Actual = new[] { 1, 2, 3 },
				Expected = null,
			};

			mb.Executing(x => x.Compose(info).Split(new[] {Environment.NewLine}, StringSplitOptions.None)).NotThrows();
		}
	}
}