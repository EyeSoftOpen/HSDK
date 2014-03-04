namespace EyeSoft.Test.IO
{
	using System.IO;

	using EyeSoft.IO;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class FileInfoWrapperTest
	{
		private const string Path = @"R:\Fake-Path\2013\19\08\AA093F6A-96E7-474A-89E9-7DBC5E9EA47F.txt";

		[TestMethod]
		public void CheckFileWrapperCreation()
		{
			IFileInfo fileInfo = null;

			Executing.This(() => fileInfo = new FileInfoWrapper(Path)).Should().NotThrow();

			fileInfo.FullName.Should().Be.EqualTo(Path);

			const string FullDirectoryName = "R:\\Fake-Path\\2013\\19\\08";
			fileInfo.DirectoryName.Should().Be.EqualTo(FullDirectoryName);
			fileInfo.Directory.Name.Should().Be.EqualTo("08");
			fileInfo.Directory.FullName.Should().Be.EqualTo(FullDirectoryName);

			fileInfo.Exists.Should().Be.False();

			const int ExpectedLength = -1;
			long length = ExpectedLength;
			Executing.This(() => { length = fileInfo.Length; }).Should().Throw<FileNotFoundException>();
			length.Should().Be.EqualTo(ExpectedLength);
		}
	}
}