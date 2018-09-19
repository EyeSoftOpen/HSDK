namespace EyeSoft.Test.Data
{
	using System.Collections.Generic;

	using EyeSoft.Data;

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