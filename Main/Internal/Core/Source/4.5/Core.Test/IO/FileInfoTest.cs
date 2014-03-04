namespace EyeSoft.Test.IO
{
	using EyeSoft.IO;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using Moq;

	using SharpTestsEx;

	[TestClass]
	public class FileInfoTest
	{
		[TestMethod]
		public void CheckFileHash()
		{
			var storage = CreateStorage();

			Storage.Reset(() => storage);

			Storage.File(@"C:\FileName.iso").Hash()
				.Should().Be.EqualTo("6c4244329888770c6fa7f3fbf1d3b8baf9ccb7d0");
		}

		private IStorage CreateStorage()
		{
			var storageMock = new Mock<IStorage>();

			var fileInfoMock = new Mock<IFileInfo>();

			fileInfoMock
				.Setup(x => x.OpenRead())
				.Returns("File content".ToStream());

			storageMock
				.Setup(x => x.File(@"C:\FileName.iso"))
				.Returns(fileInfoMock.Object);

			return storageMock.Object;
		}
	}
}