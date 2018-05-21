namespace EyeSoft.IO
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;

    [DebuggerDisplay("{FullName}")]
    internal class DirectoryInfoWrapper : FileSystemInfoWrapper, IDirectoryInfo
    {
        private readonly DirectoryInfo directoryInfo;

        public DirectoryInfoWrapper(string path) : this(new DirectoryInfo(path))
        {
        }

        public DirectoryInfoWrapper(DirectoryInfo directoryInfo) : base(directoryInfo)
        {
            this.directoryInfo = directoryInfo;
        }

        public IDirectoryInfo Parent
        {
            get
            {
                return directoryInfo.Parent == null ? null : new DirectoryInfoWrapper(directoryInfo.Parent.FullName);
            }
        }

        public void Create()
        {
            directoryInfo.Create();
        }

        public void Delete(bool recursive)
        {
            directoryInfo.Delete(recursive);
        }

        public void DeleteIfExists(bool recursive)
        {
            if (!directoryInfo.Exists)
            {
                return;
            }

            directoryInfo.Delete(recursive);
        }

        public void DeleteSubFolders()
        {
            if (!directoryInfo.Exists)
            {
                return;
            }

            foreach (var subDirectory in directoryInfo.EnumerateDirectories())
            {
                subDirectory.Delete(true);
            }
        }

        public IFileInfo[] GetFiles()
        {
            return GetFiles("*.*");
        }

        public IFileInfo[] GetFiles(string searchPattern)
        {
            return GetFiles(searchPattern, SearchOption.TopDirectoryOnly);
        }

        public IFileInfo[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            var files =
                directoryInfo
                    .GetFiles(searchPattern, searchOption)
                    .Select(file => (IFileInfo)new FileInfoWrapper(file.FullName))
                    .ToArray();

            return files;
        }

        public IDirectoryInfo[] GetDirectories()
        {
            return GetDirectories("*.*");
        }

        public IDirectoryInfo[] GetDirectories(string searchPattern)
        {
            return GetDirectories("*.*", SearchOption.TopDirectoryOnly);
        }

        public IDirectoryInfo[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            var directories =
                directoryInfo
                    .GetDirectories(searchPattern, searchOption)
                    .Select(file => (IDirectoryInfo)new DirectoryInfoWrapper(file.FullName))
                    .ToArray();

            return directories;
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories()
        {
            return directoryInfo.EnumerateDirectories().Select(x => new DirectoryInfoWrapper(x));
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern)
        {
            return directoryInfo.EnumerateDirectories(searchPattern).Select(x => new DirectoryInfoWrapper(x));
        }

        public IEnumerable<IDirectoryInfo> EnumerateDirectories(string searchPattern, SearchOption searchOption)
        {
            return directoryInfo.EnumerateDirectories(searchPattern, searchOption).Select(x => new DirectoryInfoWrapper(x));
        }

        public IEnumerable<IFileInfo> EnumerateFiles()
        {
            return directoryInfo.EnumerateFiles().Select(x => new FileInfoWrapper(x));
        }

        public IEnumerable<IFileInfo> EnumerateFiles(string searchPattern)
        {
            return directoryInfo.EnumerateFiles(searchPattern).Select(x => new FileInfoWrapper(x));
        }

        public IEnumerable<IFileInfo> EnumerateFiles(string searchPattern, SearchOption searchOption)
        {
            return directoryInfo.EnumerateFiles(searchPattern, searchOption).Select(x => new FileInfoWrapper(x));
        }
    }
}