namespace EyeSoft.Core.Test.Data
{
    using System;
    using System.Diagnostics;
    using Core.Data;
    using Core.IO;
    using Helpers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using SharpTestsEx;

    [TestClass]
	public class ApplicationDataTest
	{
		private readonly ApplicationDataSettings<Foo> dataSettings;

		private readonly DataTestStorage storage;

		public ApplicationDataTest()
		{
			storage = new DataTestStorage(true);
			Storage.Reset(() => storage);

			dataSettings = new ApplicationInfo("EyeSoft", "Test").Settings<Foo>(true);
		}

		[TestMethod]
		public void SaveAndLoadProtecedDataOfExistingFileVerifyMatch()
		{
			var original = new Foo { Property = "Value1" };

			const int CryptedBytesCount = 310;

			var stopwatch = Stopwatch.StartNew();
			dataSettings.Save(original);
			stopwatch.Stop();
			Console.WriteLine("Save data from string: {0}(ms)", stopwatch.ElapsedMilliseconds);

			stopwatch.Restart();
			var decrypted = dataSettings.Load();
			Console.WriteLine("Load data from string: {0}(ms)", stopwatch.ElapsedMilliseconds);

			storage.WritePath.Should().EndWith(@"\EyeSoft\Test\Foo.xml.secure");
			storage.WriteBytes.Should().Have.Count.EqualTo(CryptedBytesCount);

			storage.ReadPath.Should().Be.EqualTo(storage.WritePath);
			storage.ReadBytes.Should().Have.Count.EqualTo(CryptedBytesCount);

			decrypted.Should().Be.EqualTo(original);
		}

		[TestMethod]
		public void LoadProtecedDataOfNotExistingFileVerifyDataIsNull()
		{
			// ReSharper disable once RedundantAssignment
			var dataTestStorage = new DataTestStorage(false);

			Storage.Reset(() => dataTestStorage);

			dataSettings.Load().Should().Be.Null();
		}

		[Serializable]
		public class Foo
		{
			public string Property { get; set; }

			public override bool Equals(object obj)
			{
				if (obj == null || GetType() != obj.GetType())
				{
					return false;
				}

				var other = (Foo)obj;
				return Property.Equals(other.Property);
			}

			public override int GetHashCode()
			{
				return Property.GetHashCode();
			}
		}
	}
}