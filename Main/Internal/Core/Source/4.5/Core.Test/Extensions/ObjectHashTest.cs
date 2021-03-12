namespace EyeSoft.Core.Test
{
    using System;
    using Extensions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ObjectHashTest
	{
		private const int ExpectedHashCode = -842343738;

		private const int ExpectedHashCodeOrderChanged = -842352753;

		private const int Value1 = 1;
		private const string Value2 = "2";

		//TODO: in .NET Core the hash of an object changes everytime it is calculated
		//[TestMethod]
		//public void CheckCombinedHashCodesIsCorrect()
		//{
		//	ObjectHash.Combine(Value1.GetHashCode(), Value2.GetHashCode()).Should().Be(ExpectedHashCode);
		//}

		//[TestMethod]
		//public void CheckCombinedInvariantHashCodesInDifferentOrderIsCorrect()
		//{
		//	ObjectHash.CombineInvariant(Value1.GetHashCode(), Value2.GetHashCode()).Should().Be(ExpectedHashCodeOrderChanged);

		//	ObjectHash.CombineInvariant(Value2.GetHashCode(), Value1.GetHashCode()).Should().Be(ExpectedHashCodeOrderChanged);
		//}

		//[TestMethod]
		//public void CheckCombinedHashCodesFromIstancesIsCorrect()
		//{
		//	ObjectHash.Combine(Value1, Value2).Should().Be(ExpectedHashCode);
		//}

		//[TestMethod]
		//public void CheckCombinedInvariantHashCodesFromIstancesInDifferentOrderIsCorrect()
		//{
		//	ObjectHash.CombineInvariant(Value1, Value2).Should().Be(ExpectedHashCodeOrderChanged);

		//	ObjectHash.CombineInvariant(Value2, Value1).Should().Be(ExpectedHashCodeOrderChanged);
		//}

		//[TestMethod]
		//public void CheckCombinedHashCodesFromIstancesWithNullValues()
		//{
		//	ObjectHash.CombineInvariant(new[] { "test", null }).Should().Be(-354185609);
		//}

		[TestMethod]
		public void CheckCombinedHashCodesFromIstancesWithOnlyNullValues()
		{
			Action emptyObject = () => ObjectHash.CombineInvariant((object)null).Should().Be(ExpectedHashCodeOrderChanged);

            emptyObject.Should().Throw<InvalidOperationException>();
		}
	}
}