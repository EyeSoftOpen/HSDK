namespace EyeSoft.Core.IO
{
    using System.IO;

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

        public long Length => fileInfo.Length;

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