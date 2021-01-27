namespace EyeSoft.Core.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Compression;

    public static class Storage
	{
		public const string AllPattern = "*.*";

		private static readonly Singleton<IStorage> singletonInstance = new Singleton<IStorage>(() => new FileSystemStorage());

		public static void Set(Func<IStorage> storage)
		{
			singletonInstance.Set(storage);
		}

		public static IDirectoryInfo Directory(string path)
		{
			return singletonInstance.Instance.Directory(path);
		}

		public static IFileInfo File(string path)
		{
			return singletonInstance.Instance.File(path);
		}

		public static IEnumerable<IFileInfo> GetFiles(string path, string searchPattern)
		{
			return singletonInstance.Instance.GetFiles(path, searchPattern);
		}

		public static void WriteAllBytes(string path, byte[] bytes)
		{
			singletonInstance.Instance.WriteAllBytes(path, bytes);
		}

		public static void WriteAllText(string path, string contents, bool overwrite = false)
		{
			WriteAllText(path, contents, Encoding.Default, overwrite);
		}

		public static void WriteAllText(string path, string contents, Encoding encoding, bool overwrite = false)
		{
			encoding = encoding ?? Encoding.Default;

			singletonInstance.Instance.WriteAllText(path, contents, encoding, overwrite);
		}

		public static Stream OpenWrite(string path)
		{
			return singletonInstance.Instance.OpenWrite(path);
		}

		public static Stream OpenRead(string path)
		{
			return singletonInstance.Instance.OpenRead(path);
		}

		public static void SaveToFile(this Stream stream, string path)
		{
			singletonInstance.Instance.SaveStreamToFile(stream, path);
		}

		public static string ReadAllText(string path)
		{
			return singletonInstance.Instance.ReadAllText(path, Encoding.Default);
		}

		public static string ReadAllText(string path, Encoding encoding)
		{
			encoding = encoding ?? Encoding.Default;

			var text = singletonInstance.Instance.ReadAllText(path, encoding);
			return text;
		}

		public static byte[] ReadAllBytes(string path)
		{
			return singletonInstance.Instance.ReadAllBytes(path);
		}

		public static bool Exists(string path)
		{
			return singletonInstance.Instance.Exists(path);
		}

		public static void Delete(string path)
		{
			singletonInstance.Instance.Delete(path);
		}

		public static void CompressFile(string sourcePath, string destinationPath, ICompressor compressor = null)
		{
			singletonInstance.Instance.CompressFile(sourcePath, destinationPath);
		}

		public static void DecompressFile(string sourcePath, string destinationPath, ICompressor decompressor = null)
		{
			singletonInstance.Instance.DecompressFile(sourcePath, destinationPath);
		}

		public static void Reset(Func<IStorage> func)
		{
			singletonInstance.Reset(func);
		}
	}
}