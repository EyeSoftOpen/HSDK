namespace EyeSoft.Core.Test.Data
{
    using System;
    using Core.Data;
    using Core.IO;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class ApplicationDataSimplifiedTest
	{
		private readonly ApplicationDataSettings<Person> dataSettings =
			new ApplicationInfo("EyeSoft", "Test").Settings<Person>();

		[TestMethod]
		public void SaveAndLoadProtecedDataOfExistingFileVerifyMatch()
		{
			Storage.Reset(() => new DataTestStorage(true));

			const string ExpectedName = "Bill";

			dataSettings.Save(new Person(ExpectedName));

			var loaded = dataSettings.Load();

			loaded.Name.Should().Be.EqualTo(ExpectedName);
		}

		[Serializable]
		public class Person
		{
			public Person(string name)
			{
				Name = name;
			}

			private Person()
			{
			}

			public string Name { get; set; }
		}
	}
}