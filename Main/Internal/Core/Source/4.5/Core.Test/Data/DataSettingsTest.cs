namespace EyeSoft.Core.Test.Data
{
    using System.Collections.Generic;
    using Core.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class DataSettingsTest
	{
		[TestMethod]
		public void VerifyTheKeyByTypeNameUsingGenericsIsAValidFileName()
		{
			DataSettingsKey.KeyOrTypeName<List<string>>().Should().Be.EqualTo("List'string'");
		}
	}
}