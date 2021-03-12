namespace EyeSoft.Core.Test.Data
{
    using System;
    using EyeSoft.Data;
    using EyeSoft.IO;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class ApplicationInfoDataSettingsPathTest
	{
        public ApplicationInfoDataSettingsPathTest()
        {
            var storage = new DataTestStorage(true);
            Storage.Reset(() => storage);
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

            Action action = () => new DataSettingsConfiguration(new ApplicationData(info, DataScope.CurrentUser, subFolders), true, "MyData");

			action.Should().Throw<ArgumentException>();
		}
	}
}