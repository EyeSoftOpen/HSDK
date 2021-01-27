namespace SharpTestsEx.Test.ExtensionsImpl
{
    using System;
    using NUnit.Framework;
    using SharpTestsEx.ExtensionsImpl;

    public class NumericsComparerTest
	{
		[Test]
		public void CanCompareDifferentNumericTypes()
		{
			var nc = new NumericsComparer();
			Assert.AreEqual(0, nc.Compare(5, 5L));
			Assert.AreEqual(0, nc.Compare(5, 5D));
			Assert.AreEqual(0, nc.Compare(5, (uint)5));
			Assert.AreEqual(0, nc.Compare(5m, (uint)5));
			Assert.AreEqual(0, nc.Compare(5m, 5D));
		}

		[Test]
		public void WithNulls()
		{
			var nc = new NumericsComparer();
			Assert.AreEqual(0, nc.Compare(null, null));
			Assert.AreEqual(1, nc.Compare(5, null));
			Assert.AreEqual(-1, nc.Compare(null, 5));
		}

		private class A { }
		[Test]
		public void CompareNotComparable()
		{
			var nc = new NumericsComparer();
			var m= nc.Executing(x => x.Compare(new A(), new A())).Throws<ArgumentException>().Exception.Message;
			StringAssert.Contains("not comparables", m);
		}

		[Test]
		public void CompareWithNotNumericType()
		{
			var nc = new NumericsComparer();
			var m = nc.Executing(x => x.Compare(5, DateTime.Today)).Throws<ArgumentException>().Exception.Message;
			StringAssert.Contains("not comparables", m);
		}

		[Test]
		public void CompareIComparableNoNumeric()
		{
			var nc = new NumericsComparer();
			nc.Executing(x => x.Compare(DateTime.Today, DateTime.Now)).NotThrows();
			Assert.AreEqual(0, nc.Compare(DateTime.Today, DateTime.Today));
		}
	}
}