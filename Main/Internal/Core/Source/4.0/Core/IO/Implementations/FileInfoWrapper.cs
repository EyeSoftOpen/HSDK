namespace EyeSoft.IO
{
	using System.IO;
	using System.Security.AccessControl;

	internal sealed class FileInfoWrapper : FileSystemInfoWrapper, IFileInfo
	{
		private readonly FileInfo fileInfo;

		public FileInfoWrapper(string fileName) : this(new FileInfo(fileName))
		{
		}

		public FileInfoWrapper(FileInfo fileInfo) : base(fileInfo)
		{
			this.fileInfo = fileInfo;

			DirectoryName = fileInfo.DirectoryName;
			Directory = new DirectoryInfoWrapper(fileInfo.DirectoryName);
		}

		public long Length
		{
			get { return fileInfo.Length; }
		}

		public string DirectoryName { get; private set; }

		public IDirectoryInfo Directory { get; private set; }

		public IFileInfo CopyTo(string destination)
		{
			return new FileInfoWrapper(fileInfo.CopyTo(destination));
		}

		public IFileInfo CopyTo(string destination, bool overwrite)
		{
			return new FileInfoWrapper(fileInfo.CopyTo(destination, overwrite));
		}

		public void MoveTo(string destination)
		{
			fileInfo.MoveTo(destination);
		}

		public FileSystemSecurity GetAccessControl()
		{
			return fileInfo.GetAccessControl();
		}

		public FileSystemSecurity GetAccessControl(AccessControlSections includeSections)
		{
			return fileInfo.GetAccessControl(includeSections);
		}

		public Stream OpenRead()
		{
			return fileInfo.OpenRead();
		}

		public Stream OpenWrite()
		{
			return fileInfo.OpenWrite();
		}
	}
}