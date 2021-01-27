namespace EyeSoft.Core.Test.Mapping.Strategies
{
    using System.Collections.Generic;
    using Core.Mapping.Strategies;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
	public class ReferencePropertyStrategyTest
		: PropertyStrategyTest<ReferenceMemberStrategy>
	{
		[TestMethod]
		public void ExtractPrimitivePropertyExptectedReturnsFalse()
		{
			CheckProperty<string>(false);
		}

		[TestMethod]
		public void ExtractReferencePropertyExptectedReturnsTrue()
		{
			CheckProperty<Customer>(true);
		}

		[TestMethod]
		public void ExtractCollectionPropertyExptectedReturnsFalse()
		{
			CheckProperty<List<Customer>>(false);
		}
	}
}