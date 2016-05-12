namespace EyeSoft.Security.Cryptography
{
	using System.IO;
	using System.Security.Cryptography;

	internal class HashAlgorithmWrapper : IHashAlgorithm
	{
		private readonly HashAlgorithm hashAlgorithm;

		public HashAlgorithmWrapper(HashAlgorithm hashAlgorithm)
		{
			Enforce.Argument(() => hashAlgorithm);

			this.hashAlgorithm = hashAlgorithm;
		}

		public byte[] ComputeHash(Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);
			var computeHash = hashAlgorithm.ComputeHash(stream);

			return computeHash;
		}

		public byte[] ComputeHash(byte[] buffer)
		{
			return hashAlgorithm.ComputeHash(buffer);
		}

		public byte[] ComputeHash(byte[] buffer, int offset, int count)
		{
			return hashAlgorithm.ComputeHash(buffer, offset, count);
		}

		public void Dispose()
		{
			hashAlgorithm.Dispose();
		}
	}
}