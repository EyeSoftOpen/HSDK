namespace EyeSoft.IO.Compression
{
    using System;
    using System.IO;

    public interface ICompressor : IDisposable
	{
		Stream Compress(Stream source);

		Stream Decompress(Stream source);
	}
}
