namespace EyeSoft.Core.Test.Mapping.Strategies
{
    using System.ComponentModel.DataAnnotations;
    using Core.Mapping.Strategies;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
	public class OnlyValidablePrimitivePropertyStrategyTest
		: PropertyStrategyTest<OnlyValidablePrimitiveMemberStrategy>
	{
		[TestMethod]
		public void ExtractValidablePrimitivePropertiesOnNotValidableProperty()
		{
			CheckProperty<string>(false);
		}

		[TestMethod]
		public void ExtractValidablePrimitivePropertiesOnValidableProperty()
		{
			CheckProperty<string>(true, new StringLengthAttribute(50));
		}
	}
}