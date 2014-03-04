namespace EyeSoft.Test.Data
{
	using System;

	using EyeSoft.Data;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class ApplicationInfoTest
	{
		private readonly ApplicationInfo applicationInfo = new ApplicationInfo("EyeSoft", "Test");

		[TestMethod]
		public void VerifyCannotChangeApplicationDataSettings()
		{
			applicationInfo.Settings<Foo>(new[] { "Folder" });

			Action register = () => applicationInfo.Settings<Foo>(true, new[] { "Folder" });

			Executing.This(register).Should().Throw<InvalidOperationException>();
		}

		[TestMethod]
		public void VerifyApplicationInfoAndApplicationDataGiveTheSameSettings()
		{
			var basePath = applicationInfo.Data();
			var usersPath = basePath.Append("Users");

			var applicationDataSettings = usersPath.Settings<Foo>();
			var existingApplicationDataSettings = applicationInfo.Settings<Foo>(new[] { "Users" });

			applicationDataSettings.Should().Be.SameInstanceAs(existingApplicationDataSettings);

			var pathFromApplicationData = applicationDataSettings.Configuration.Path;
			var pathFromApplicationInfo = existingApplicationDataSettings.Configuration.Path;

			pathFromApplicationData.Should().Be.EqualTo(pathFromApplicationInfo);
		}

		private class Foo
		{
		}
	}
}