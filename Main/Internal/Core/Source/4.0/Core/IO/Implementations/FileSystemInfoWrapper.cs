namespace EyeSoft.Core.IO
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using Extensions;

    [DebuggerDisplay("{FullName}")]
	internal abstract class FileSystemInfoWrapper : IFileSystemInfo
	{
		private readonly FileSystemInfo fileSystemInfo;

		protected FileSystemInfoWrapper(FileSystemInfo fileSystemInfo)
		{
			this.fileSystemInfo = fileSystemInfo;
		}

		public FileAttributes Attributes
		{
			get { return fileSystemInfo.Attributes; }
			set { fileSystemInfo.Attributes = value; }
		}

		public string Name
		{
			get { return fileSystemInfo.Name; }
		}

		public string FullName
		{
			get { return fileSystemInfo.FullName; }
		}

		public bool Exists
		{
			get { return fileSystemInfo.Exists; }
		}

		public string Extension
		{
			get { return fileSystemInfo.Extension; }
		}

		public DateTime CreationTime
		{
			get { return fileSystemInfo.CreationTime; }
			set { fileSystemInfo.CreationTime = value; }
		}

		public DateTime CreationTimeUtc
		{
			get { return fileSystemInfo.CreationTimeUtc; }
			set { fileSystemInfo.CreationTimeUtc = value; }
		}

		public DateTime LastAccessTime
		{
			get { return fileSystemInfo.LastAccessTime; }
			set { fileSystemInfo.LastAccessTime = value; }
		}

		public DateTime LastAccessTimeUtc
		{
			get { return fileSystemInfo.LastAccessTimeUtc; }
			set { fileSystemInfo.LastAccessTimeUtc = value; }
		}

		public DateTime LastWriteTime
		{
			get { return fileSystemInfo.LastWriteTime; }
			set { fileSystemInfo.LastWriteTime = value; }
		}

		public DateTime LastWriteTimeUtc
		{
			get { return fileSystemInfo.LastWriteTimeUtc; }
			set { fileSystemInfo.LastWriteTimeUtc = value; }
		}

		public void Delete()
		{
			fileSystemInfo.Delete();
		}

		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
			{
				return false;
			}

			var other = (IFileSystemInfo)obj;
			return FullName.IgnoreCaseEquals(other.FullName);
		}

		public override int GetHashCode()
		{
			return FullName.GetHashCode();
		}
	}
}