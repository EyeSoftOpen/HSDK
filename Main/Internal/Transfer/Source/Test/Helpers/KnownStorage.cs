namespace EyeSoft.Transfer.Service.Helpers
{
	using System;
	using System.IO;

	using EyeSoft.IO;

	using Moq;

	internal static class KnownStorage
	{
		public static IStorage Mock
		{
			get
			{
				return GetStorage();
			}
		}

		private static IStorage GetStorage()
		{
			var mockStorage = new Mock<IStorage>();
			var fileInfo = GetFileInfo();

			Action<string> checkPath = path => (!path.Equals(KnownFile.Path)).OnTrue().Throw(new FileNotFoundException());

			mockStorage
				.Setup(storage => storage.GetFileInfo(It.IsAny<string>()))
				.Callback(checkPath)
				.Returns(fileInfo);

			mockStorage
				.Setup(storage => storage.OpenRead(It.IsAny<string>()))
				.Callback(checkPath)
				.Returns(fileInfo.OpenRead());

			return mockStorage.Object;
		}

		private static IFileInfo GetFileInfo()
		{
			var mockFileInfo = new Mock<IFileInfo>();

			mockFileInfo
				.SetupGet(fileInfo => fileInfo.Length)
				.Returns(KnownFile.DocumentSize);

			mockFileInfo
				.Setup(fileInfo => fileInfo.OpenRead())
				.Returns(KnownFile.Stream);

			return mockFileInfo.Object;
		}
	}
}