namespace EyeSoft.Core.Test.Data.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using EyeSoft.IO;
    using Moq;

    internal class DataTestStorage : DefaultStorage
	{
		private readonly bool fileExists;

		public DataTestStorage(bool fileExists)
		{
			this.fileExists = fileExists;
		}

		public string WritePath { get; private set; }

		public string ReadPath { get; private set; }

		public byte[] WriteBytes { get; private set; }

		public byte[] ReadBytes { get; private set; }

		public override IDirectoryInfo Directory(string path)
		{
			return new Mock<IDirectoryInfo>().Object;
		}

		public override IFileInfo File(string path)
		{
			ReadPath = path;

			var fileInfoMock = new Mock<IFileInfo>();
			fileInfoMock.SetupGet(x => x.Exists).Returns(fileExists);

			var directoryInfoMock = new Mock<IDirectoryInfo>();

			directoryInfoMock.SetupGet(x => x.FullName).Returns(Path.GetDirectoryName(path));

			fileInfoMock.SetupGet(x => x.Directory).Returns(directoryInfoMock.Object);

			return fileInfoMock.Object;
		}

		public override void WriteAllBytes(string path, byte[] bytes)
		{
			WritePath = path;
			WriteBytes = bytes;
		}

		public override void WriteAllText(string path, string contents, Encoding encoding = null, bool overwrite = false)
		{
			throw new NotImplementedException();
		}

		public override byte[] ReadAllBytes(string path)
		{
			ReadPath = path;
			ReadBytes = WriteBytes;

			return ReadBytes;
		}

		public override void SaveStreamToFile(Stream stream, string path)
		{
			throw new NotImplementedException();
		}

		public override Stream OpenWrite(string path)
		{
			throw new NotImplementedException();
		}

		public override Stream OpenRead(string path)
		{
			throw new NotImplementedException();
		}

		public override IEnumerable<IFileInfo> GetFiles(string path, string searchPattern)
		{
			throw new NotImplementedException();
		}

		public override string ReadAllText(string path, Encoding encoding)
		{
			throw new NotImplementedException();
		}

		public override bool Exists(string path)
		{
			throw new NotImplementedException();
		}

		public override void Delete(string path)
		{
			throw new NotImplementedException();
		}

		public string GetFileNameWithoutExtension(string path)
		{
			throw new NotImplementedException();
		}
	}
}