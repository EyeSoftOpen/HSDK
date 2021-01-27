namespace EyeSoft.Core.Test.Mapping.Strategies
{
    using System.Collections.Generic;
    using Core.Mapping.Strategies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
	public class CollectionPropertyStrategyTest
		: PropertyStrategyTest<CollectionMemberStrategy>
	{
		[TestMethod]
		public void NotExtractCollectionOfPrimitiveTypes()
		{
			CheckProperty<List<string>>(false);
		}

		[TestMethod]
		public void ExtractCollectionOfNotPrimitiveTypes()
		{
			CheckProperty<IEnumerable<School>>(true);
		}

		[TestMethod]
		public void ExtractEnumerableOfNotPrimitiveTypes()
		{
			CheckProperty<List<School>>(true);
		}

		[TestMethod]
		public void NotExtractReadOnlyCollections()
		{
			CheckProperty<School>(false);
		}

		public class School
		{
		}
	}
}