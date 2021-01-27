namespace EyeSoft.Core.Security.Cryptography
{
    using System;
    using System.IO;

    public interface IHashAlgorithm : IDisposable
	{
		byte[] ComputeHash(Stream stream);

		byte[] ComputeHash(byte[] buffer);

		byte[] ComputeHash(byte[] buffer, int offset, int count);
	}
}