namespace EyeSoft.IO
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
			get => fileSystemInfo.Attributes;
            set => fileSystemInfo.Attributes = value;
        }

		public string Name => fileSystemInfo.Name;

        public string FullName => fileSystemInfo.FullName;

        public bool Exists => fileSystemInfo.Exists;

        public string Extension => fileSystemInfo.Extension;

        public DateTime CreationTime
		{
			get => fileSystemInfo.CreationTime;
            set => fileSystemInfo.CreationTime = value;
        }

		public DateTime CreationTimeUtc
		{
			get => fileSystemInfo.CreationTimeUtc;
            set => fileSystemInfo.CreationTimeUtc = value;
        }

		public DateTime LastAccessTime
		{
			get => fileSystemInfo.LastAccessTime;
            set => fileSystemInfo.LastAccessTime = value;
        }

		public DateTime LastAccessTimeUtc
		{
			get => fileSystemInfo.LastAccessTimeUtc;
            set => fileSystemInfo.LastAccessTimeUtc = value;
        }

		public DateTime LastWriteTime
		{
			get => fileSystemInfo.LastWriteTime;
            set => fileSystemInfo.LastWriteTime = value;
        }

		public DateTime LastWriteTimeUtc
		{
			get => fileSystemInfo.LastWriteTimeUtc;
            set => fileSystemInfo.LastWriteTimeUtc = value;
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