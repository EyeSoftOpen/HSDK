namespace EyeSoft.Core.Test.Data
{
    using System.Collections.Generic;
    using EyeSoft.Data;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class DataSettingsTest
	{
		[TestMethod]
		public void VerifyTheKeyByTypeNameUsingGenericsIsAValidFileName()
		{
			DataSettingsKey.KeyOrTypeName<List<string>>().Should().Be("List'string'");
		}
	}
}