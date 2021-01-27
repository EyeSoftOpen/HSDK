namespace EyeSoft.Core.IO
{
    using System.IO;

    public interface IFileInfo : IFileSystemInfo
    {
        long Length { get; }

        string DirectoryName { get; }

        IDirectoryInfo Directory { get; }

        IFileInfo CopyTo(string destination);

        IFileInfo CopyTo(string destination, bool overwrite);

        void MoveTo(string destination);

        Stream OpenRead();

        Stream OpenWrite();
    }
}