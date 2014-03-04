namespace EyeSoft.Testing.Test.IO
{
	using EyeSoft.Extensions;
	using EyeSoft.IO;
	using EyeSoft.Testing.IO;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class MemoryStorageTest
	{
		[TestMethod]
		public void WriteFileUsingMemoryStorageProviderReadContentsExpectedAreCorrect()
		{
			IStorage memoryStorage = new MemoryStorage();

			const string FileName = "temp1.text";
			const string Contents = "hello!";

			memoryStorage.WriteAllText(FileName, Contents);

			memoryStorage
				.File(FileName)
				.OpenRead()
				.Using(
					a =>
					a
						.StreamToString()
						.Should("The file content of the MemoryStorageProvider is not correct.")
						.Be
						.EqualTo(Contents));
		}
	}
}