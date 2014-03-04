namespace EyeSoft.Test
{
	using System;

	using EyeSoft.Extensions;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ObjectHashTest
	{
		private const int ExpectedHashCode = -842343738;

		private const int ExpectedHashCodeOrderChanged = -842352753;

		private const int Value1 = 1;
		private const string Value2 = "2";

		[TestMethod]
		public void CheckCombinedHashCodesIsCorrect()
		{
			ObjectHash.Combine(Value1.GetHashCode(), Value2.GetHashCode()).Should().Be.EqualTo(ExpectedHashCode);
		}

		[TestMethod]
		public void CheckCombinedInvariantHashCodesInDifferentOrderIsCorrect()
		{
			ObjectHash.CombineInvariant(Value1.GetHashCode(), Value2.GetHashCode()).Should().Be.EqualTo(ExpectedHashCodeOrderChanged);

			ObjectHash.CombineInvariant(Value2.GetHashCode(), Value1.GetHashCode()).Should().Be.EqualTo(ExpectedHashCodeOrderChanged);
		}

		[TestMethod]
		public void CheckCombinedHashCodesFromIstancesIsCorrect()
		{
			ObjectHash.Combine(Value1, Value2).Should().Be.EqualTo(ExpectedHashCode);
		}

		[TestMethod]
		public void CheckCombinedInvariantHashCodesFromIstancesInDifferentOrderIsCorrect()
		{
			ObjectHash.CombineInvariant(Value1, Value2).Should().Be.EqualTo(ExpectedHashCodeOrderChanged);

			ObjectHash.CombineInvariant(Value2, Value1).Should().Be.EqualTo(ExpectedHashCodeOrderChanged);
		}

		[TestMethod]
		public void CheckCombinedHashCodesFromIstancesWithOnlyNullValues()
		{
			Action emptyObject =
				() => ObjectHash.CombineInvariant(new[] { (object)null }).Should().Be.EqualTo(ExpectedHashCodeOrderChanged);
			
			Executing.This(emptyObject).Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void CheckCombinedHashCodesFromIstancesWithNullValues()
		{
			ObjectHash.CombineInvariant(new[] { "test", (object)null }).Should().Be.EqualTo(-354185609);
		}
	}
}