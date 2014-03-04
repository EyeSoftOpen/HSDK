namespace EyeSoft.IO
{
	using System;
	using System.IO;
	using System.Security.AccessControl;

	public class FileInfoBase : IFileInfo
	{
		public FileInfoBase(string fullName, long length = 0)
		{
			Enforce.Argument(() => fullName);

			FullName = fullName;
			Length = length;
			DirectoryName = Path.GetDirectoryName(fullName);
		}

		public FileAttributes Attributes { get; set; }

		public string Name { get; private set; }

		public string FullName { get; private set; }

		public long Length { get; protected set; }

		public virtual string DirectoryName { get; protected set; }

		public virtual IDirectoryInfo Directory { get; protected set; }

		public virtual bool Exists { get; protected set; }

		public string Extension { get; private set; }

		public DateTime CreationTime { get; set; }

		public DateTime CreationTimeUtc { get; set; }

		public DateTime LastAccessTime { get; set; }

		public DateTime LastAccessTimeUtc { get; set; }

		public DateTime LastWriteTime { get; set; }

		public DateTime LastWriteTimeUtc { get; set; }

		public virtual IFileInfo CopyTo(string destination)
		{
			throw new NotImplementedException();
		}

		public virtual IFileInfo CopyTo(string destination, bool overwrite)
		{
			throw new NotImplementedException();
		}

		public virtual void MoveTo(string destination)
		{
			throw new NotImplementedException();
		}

		public virtual void Delete()
		{
			throw new NotImplementedException();
		}

		public virtual FileSystemSecurity GetAccessControl()
		{
			throw new NotImplementedException();
		}

		public virtual FileSystemSecurity GetAccessControl(AccessControlSections includeSections)
		{
			throw new NotImplementedException();
		}

		public virtual Stream OpenRead()
		{
			throw new NotSupportedException();
		}

		public virtual Stream OpenWrite()
		{
			throw new NotSupportedException();
		}
	}
}