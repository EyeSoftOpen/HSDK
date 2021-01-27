namespace EyeSoft.Core.Test.Logging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Core.IO;
    using Core.Logging;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SharpTestsEx;

    [TestClass]
	public class XmlFileExceptionLoggerTest
	{
		[TestMethod]
		public void LogExceptionUsingXmlExceptionLogger()
		{
			var testStorageResult = new TestStorageResult();

			Storage.Reset(testStorageResult.Storage);

			new XmlFileExceptionLogger(@"C:\Logging\Test")
				.Error(new InvalidOperationException("Custom message", new ArgumentException("Argument not valid.")));

			testStorageResult.OperationsLog.Should().Have.Count.EqualTo(4);
			testStorageResult.OperationsLog[3].Item1.Should().Be.EqualTo("Save");
		}

		private class TestStorageResult
		{
			private readonly IList<Tuple<string, string>> operationsLog = new List<Tuple<string, string>>();

			private readonly Mock<IDirectoryInfo> directoryMock = new Mock<IDirectoryInfo>();

			private string localPath;

			public IList<Tuple<string, string>> OperationsLog => operationsLog;

            public IStorage Storage()
			{
				var storageMock = new Mock<IStorage>();

				storageMock.Setup(x => x.Directory(It.IsAny<string>())).Returns<string>(GetDirectory);

				directoryMock.Setup(x => x.Create()).Callback(() => Add("Create", localPath));

				var streamMock = new Mock<Stream>();

				streamMock.Setup(x => x.Write(It.IsAny<byte[]>(), It.IsAny<int>(), It.IsAny<int>()))
					.Callback<byte[], int, int>((buffer, offset, count) => Add("Save", Encoding.ASCII.GetString(buffer, offset, count)));

				storageMock.Setup(x => x.OpenWrite(It.IsAny<string>())).Returns<string>(
					path => { Add("OpenWrite", path); return streamMock.Object; });

				return storageMock.Object;
			}

			private void Add(string operation, string description)
			{
				operationsLog.Add(new Tuple<string, string>(operation, description));
			}

			private IDirectoryInfo GetDirectory(string path)
			{
				localPath = path;
				return directoryMock.Object;
			}
		}
	}
}