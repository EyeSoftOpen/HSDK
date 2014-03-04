namespace EyeSoft.IO
{
	using System;
	using System.IO;

	public interface IFileSystemInfo
	{
		FileAttributes Attributes { get; set;  }

		string Name { get; }

		string FullName { get; }

		bool Exists { get; }

		string Extension { get; }

		DateTime CreationTime { get; set; }

		DateTime CreationTimeUtc { get; set; }

		DateTime LastAccessTime { get; set; }

		DateTime LastAccessTimeUtc { get; set; }

		DateTime LastWriteTime { get; set;  }

		DateTime LastWriteTimeUtc { get; set; }

		void Delete();
	}
}