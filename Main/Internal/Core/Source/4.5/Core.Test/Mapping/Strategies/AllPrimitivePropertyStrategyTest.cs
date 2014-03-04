namespace EyeSoft.Test.Mapping.Strategies
{
	using System.Collections.Generic;

	using EyeSoft.Mapping.Strategies;
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	[TestClass]
	public class AllPrimitivePropertyStrategyTest
		: PropertyStrategyTest<PrimitiveMemberStrategy>
	{
		[TestMethod]
		public void ExtractPrimitivePropertyOnPrimitiveTypeExpectedReturnsTrue()
		{
			CheckProperty<string>(true);
		}

		[TestMethod]
		public void VerifyByteArrayIsPrimitiveProperty()
		{
			CheckProperty<byte[]>(true);
		}

		[TestMethod]
		public void ExtractPrimitivePropertyOnReferenceTypeExpectedReturnsFalse()
		{
			CheckProperty<School>(false);
		}

		[TestMethod]
		public void ExtractPrimitivePropertyOnCollectionTypeExpectedReturnsFalse()
		{
			CheckProperty<List<School>>(false);
		}
	}
}