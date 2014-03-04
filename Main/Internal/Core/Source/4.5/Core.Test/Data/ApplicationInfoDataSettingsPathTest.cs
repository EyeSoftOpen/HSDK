namespace EyeSoft.Test.Data
{
	using System;

	using EyeSoft.Data;
	using EyeSoft.IO;
	using EyeSoft.Test.Data.Helpers;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ApplicationInfoDataSettingsPathTest
	{
		private readonly DataTestStorage storage;

		public ApplicationInfoDataSettingsPathTest()
		{
			storage = new DataTestStorage(true);
			Storage.Reset(() => storage);
		}

		[TestMethod]
		public void VerifyApplicationDataSettingsWithoutAValidAssemblyEntryThrowException()
		{
			Executing.This(() => ApplicationInfo.FromEntryAssembly())
				.Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyApplicationDataSettingsPath()
		{
			var info = new ApplicationInfo("EyeSoft", "Test");

			new DataSettingsConfiguration(new ApplicationData(info, DataScope.CurrentUser, new[] { "General", "Mail" }), true, "MyData")
				.Path.Should().EndWith(@"EyeSoft\Test\General\Mail\MyData.xml.secure");
		}

		[TestMethod]
		public void VerifyApplicationDataSettingsSubFolderCannotBeEmpty()
		{
			var info = new ApplicationInfo("EyeSoft", "Test");
			var subFolders = new[] { "General", string.Empty };

			Executing
				.This(() => new DataSettingsConfiguration(new ApplicationData(info, DataScope.CurrentUser, subFolders), true, "MyData"))
				.Should().Throw<ArgumentException>();
		}
	}
}