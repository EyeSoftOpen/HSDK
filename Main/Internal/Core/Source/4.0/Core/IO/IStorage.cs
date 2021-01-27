namespace EyeSoft.Core.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Compression;

    public interface IStorage
	{
		IFileInfo File(string path);

		IDirectoryInfo Directory(string path);

		bool Exists(string path);

		void Delete(string path);

		void WriteAllBytes(string path, byte[] bytes);

		void WriteAllText(string path, string contents, Encoding encoding = null, bool overwrite = false);

		Stream OpenWrite(string path);

		Stream OpenRead(string path);

		IEnumerable<IFileInfo> GetFiles(string path, string searchPattern);

		string ReadAllText(string path, Encoding encoding);

		byte[] ReadAllBytes(string path);

		void SaveStreamToFile(Stream stream, string path);

		void CompressFile(string sourcePath, string destinationPath, ICompressor compressor = null);

		void DecompressFile(string sourcePath, string destinationPath, ICompressor decompressor = null);
	}
}