namespace EyeSoft.Core.Test.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Extensions;
    using EyeSoft.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using SharpTestsEx;

    [TestClass]
	public class StorageTest
	{
		private const string Decompressed = @"C:\File1.txt";
		private const string Compressed = @"C:\File1.txt.gz";

		private readonly byte[] decompressedArray = "Hello world! è to test accent.".ToByteArray();
		private readonly byte[] compressedArray = { 31, 139, 8, 0, 0, 0, 0, 0, 0, 10, 243, 96, 72, 101, 200, 1, 194, 124, 6, 5, 134, 114, 32, 89, 4, 100, 167, 48, 40, 2, 121, 47, 128, 184, 4, 44, 94, 2, 84, 83, 12, 36, 21, 24, 18, 25, 146, 129, 48, 149, 33, 15, 200, 211, 99, 0, 0, 82, 31, 101, 167, 60, 0, 0, 0 };

		[TestMethod]
		public void CompressFileInMemory()
		{
			Action<IStorage> compression = storage => storage.CompressFile(Decompressed, Compressed);
			VerifyCompression(compression, decompressedArray, compressedArray, Decompressed, Compressed);
		}

		[TestMethod]
		public void DecompressFileInMemory()
		{
			Action<IStorage> compression = storage => storage.DecompressFile(Compressed, Decompressed);
			VerifyCompression(compression, compressedArray, decompressedArray, Compressed, Decompressed);
		}

		private void VerifyCompression(Action<IStorage> action, byte[] source, IEnumerable<byte> exected, string readPath, string writePath)
		{
			using (var stream = new MemoryStream())
			{
				var storage = CreateStorageForCompression(readPath, writePath, source, stream);

				action(storage);

				var result = stream.ToArray();
				result.Should().Have.SameSequenceAs(exected);
			}
		}

		private IStorage CreateStorageForCompression(string readPath, string writePath, byte[] source, Stream destinationStream)
		{
			var mock = new Mock<DefaultStorage> { CallBase = true };

			mock.Setup(x => x.OpenRead(readPath)).Returns(() => new MemoryStream(source));
			mock.Setup(x => x.OpenWrite(writePath)).Returns(() => destinationStream);

			return mock.Object;
		}
	}
}