﻿namespace EyeSoft.Core.Test.IO
{
    using EyeSoft.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class FileSystemStorageTest
	{
		[TestMethod]
		public void CompressAndDecompressOnFileSystem()
		{
			var storage = new FileSystemStorage();

			var contents = "This is a compression test. With accent è." + new string('c', 100);

			const string Source = @"Decompressed.txt";

			storage.WriteAllText(Source, contents);

			const string Compressed = @"Source.txt.gz";
			storage.CompressFile(Source, Compressed);
			storage.File(Compressed).Length.Should().Be(64);

			const string Decompressed = "Destination.txt";
			storage.DecompressFile(Compressed, Decompressed);
			storage.File(Decompressed).Length.Should().Be(contents.Length + 1);
		}
	}
}