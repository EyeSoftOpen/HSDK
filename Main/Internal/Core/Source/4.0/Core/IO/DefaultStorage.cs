namespace EyeSoft.IO
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.IO.Compression;
	using System.Text;

	using EyeSoft.IO.Compression;

	public abstract class DefaultStorage : IStorage
	{
		public abstract IFileInfo File(string path);

		public abstract IDirectoryInfo Directory(string path);

		public abstract bool Exists(string path);

		public abstract void Delete(string path);

		public abstract void WriteAllBytes(string path, byte[] bytes);

		public abstract void WriteAllText(string path, string contents, Encoding encoding = null, bool overwrite = false);

		public abstract Stream OpenWrite(string path);

		public abstract Stream OpenRead(string path);

		public abstract IEnumerable<IFileInfo> GetFiles(string path, string searchPattern);

		public abstract string ReadAllText(string path, Encoding encoding);

		public abstract byte[] ReadAllBytes(string path);

		public abstract void SaveStreamToFile(Stream stream, string path);

		public virtual void CompressFile(string sourcePath, string destinationPath, ICompressor compressor = null)
		{
			Compression(sourcePath, destinationPath, CompressionMode.Compress, compressor);
		}

		public virtual void DecompressFile(string sourcePath, string destinationPath, ICompressor compressor = null)
		{
			Compression(sourcePath, destinationPath, CompressionMode.Decompress, compressor);
		}

		public virtual void Compression(string sourcePath, string destinationPath, CompressionMode mode, ICompressor compressor = null)
		{
			compressor = compressor ?? new GzipCompressor();

			switch (mode)
			{
				case CompressionMode.Compress:
					Compress(sourcePath, destinationPath, compressor);
					return;
				case CompressionMode.Decompress:
					Decompress(sourcePath, destinationPath, compressor);
					return;
				default:
					throw new ArgumentException("The compression mode is not valid.", "mode");
			}
		}

		private void Compress(string sourcePath, string destinationPath, ICompressor compressor)
		{
			using (var originalStream = OpenRead(sourcePath))
			{
				using (var destinationStream = OpenWrite(destinationPath))
				{
					using (compressor)
					{
						using (var compression = compressor.Compress(originalStream))
						{
							compression.CopyTo(destinationStream);
						}
					}
				}
			}
		}

		private void Decompress(string sourcePath, string destinationPath, ICompressor compressor)
		{
			using (var originalStream = OpenRead(sourcePath))
			{
				using (var destinationStream = OpenWrite(destinationPath))
				{
					using (compressor)
					{
						using (var compression = compressor.Decompress(originalStream))
						{
							compression.CopyTo(destinationStream);
						}
					}
				}
			}
		}
	}
}