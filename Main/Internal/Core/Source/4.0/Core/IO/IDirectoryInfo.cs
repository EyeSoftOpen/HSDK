namespace EyeSoft.Core.IO
{
    using System.Collections.Generic;
    using System.IO;

    public interface IDirectoryInfo : IFileSystemInfo
	{
		IDirectoryInfo Parent { get; }

		IFileInfo[] GetFiles();

		IFileInfo[] GetFiles(string searchPattern);

		IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption);

		IDirectoryInfo[] GetDirectories();

		IDirectoryInfo[] GetDirectories(string searchPattern);

		IDirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption);

		IEnumerable<IDirectoryInfo> EnumerateDirectories();

		IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern);

		IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption);

		IEnumerable<IFileInfo> EnumerateFiles();

		IEnumerable<IFileInfo> EnumerateFiles(string searchPattern);

		IEnumerable<IFileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption);

		void Create();

		void Delete(bool recursive);

		void DeleteIfExists(bool recursive);

		void DeleteSubFolders();
	}
}