namespace SharpTestsEx.Test
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class EnumerableExtensionsTest
	{
		[Test]
		public void PositionOfFirstDifferenceParameters()
		{
			((IEnumerable<int>)null).Executing(x => x.PositionOfFirstDifference(new int[0])).Throws<ArgumentNullException>();
			(new int[0]).Executing(x => x.PositionOfFirstDifference(null)).Throws<ArgumentNullException>();
		}

		[Test]
		public void PositionOfFirstDifference()
		{
			((new int[0]).PositionOfFirstDifference(new int[0])).Should().Be.EqualTo(-1);
			((new[] { 1 }).PositionOfFirstDifference(new[] { 1 })).Should().Be.EqualTo(-1);
			((new int[0]).PositionOfFirstDifference(new[] { 1 })).Should().Be.EqualTo(0);
			((new[] { 1 }).PositionOfFirstDifference(new int[0])).Should().Be.EqualTo(0);

			((new[] { 1, 2 }).PositionOfFirstDifference(new[] { 1, 1 })).Should().Be.EqualTo(1);
			((new[] { 1, 2 }).PositionOfFirstDifference(new[] { 1, 2, 3 })).Should().Be.EqualTo(2);
			((new[] { 1, 2, 3 }).PositionOfFirstDifference(new[] { 1, 2 })).Should().Be.EqualTo(2);
		}
	}
}