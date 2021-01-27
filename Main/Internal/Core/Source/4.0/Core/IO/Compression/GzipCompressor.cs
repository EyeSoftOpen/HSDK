namespace EyeSoft.Core.IO.Compression
{
    using System.IO;
    using System.IO.Compression;

    public class GzipCompressor : ICompressor
	{
		public Stream Compress(Stream source)
		{
			return new MemoryStream(Compress(source.ToByteArray()));
		}

		public Stream Decompress(Stream source)
		{
			return new MemoryStream(Decompress(source.ToByteArray()));
		}

		public void Dispose()
		{
		}

		private byte[] Compress(byte[] input)
		{
			using (var output = new MemoryStream())
			{
				using (var zip = new GZipStream(output, CompressionMode.Compress))
				{
					zip.Write(input, 0, input.Length);
				}
				return output.ToArray();
			}
		}

		private byte[] Decompress(byte[] input)
		{
			using (var stream = new GZipStream(new MemoryStream(input), CompressionMode.Decompress))
			{
				const int Size = 4096;
				var buffer = new byte[Size];

				using (var memory = new MemoryStream())
				{
					int count;

					do
					{
						count = stream.Read(buffer, 0, Size);
						if (count > 0)
						{
							memory.Write(buffer, 0, count);
						}
					}
					while (count > 0);

					return memory.ToArray();
				}
			}
		}
	}
}