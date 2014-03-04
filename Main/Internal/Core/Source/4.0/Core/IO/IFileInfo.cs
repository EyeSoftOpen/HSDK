namespace EyeSoft.IO
{
	using System.IO;
	using System.Security.AccessControl;

	public interface IFileInfo : IFileSystemInfo
	{
		long Length { get; }

		string DirectoryName { get; }

		IDirectoryInfo Directory { get; }

		IFileInfo CopyTo(string destination);

		IFileInfo CopyTo(string destination, bool overwrite);

		void MoveTo(string destination);

		FileSystemSecurity GetAccessControl();

		FileSystemSecurity GetAccessControl(AccessControlSections includeSections);

		Stream OpenRead();

		Stream OpenWrite();
	}
}