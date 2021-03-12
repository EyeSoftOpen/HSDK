namespace EyeSoft.Core.Test.IO
{
    using System;
    using System.IO;
    using EyeSoft.IO;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using FluentAssertions;

    [TestClass]
	public class FileInfoWrapperTest
	{
		private const string Path = @"R:\Fake-Path\2013\19\08\AA093F6A-96E7-474A-89E9-7DBC5E9EA47F.txt";

		[TestMethod]
		public void CheckFileWrapperCreation()
		{
			IFileInfo fileInfo = null;

            Action create = () => fileInfo = new FileInfoWrapper(Path);

            create.Should().NotThrow();

			fileInfo.FullName.Should().Be(Path);

			const string FullDirectoryName = "R:\\Fake-Path\\2013\\19\\08";
			fileInfo.DirectoryName.Should().Be(FullDirectoryName);
			fileInfo.Directory.Name.Should().Be("08");
			fileInfo.Directory.FullName.Should().Be(FullDirectoryName);

			fileInfo.Exists.Should().BeFalse();

			const int ExpectedLength = -1;
			long length = ExpectedLength;

            Action action = () => { length = fileInfo.Length; };
            
            action.Should().Throw<FileNotFoundException>();
			length.Should().Be(ExpectedLength);
		}
	}
}