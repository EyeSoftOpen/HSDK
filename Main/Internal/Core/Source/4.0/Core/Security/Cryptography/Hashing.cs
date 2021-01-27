namespace EyeSoft.Core.Security.Cryptography
{
    using System;
    using System.IO;

    public static class Hashing
	{
		private static readonly DefaultHashing hashing = new DefaultHashing();

		public static IHashAlgorithm Default => hashing.Default();

        public static IHashAlgorithm Md5 => hashing.Md5;

        public static IHashAlgorithm Sha1 => hashing.Sha1;

        public static IHashAlgorithm Sha256 => hashing.Sha256;

        public static IHashAlgorithm Sha384 => hashing.Sha384;

        public static IHashAlgorithm Sha512 => hashing.Sha512;

        public static string ComputeHex(byte[] buffer)
		{
			using (var hashAlgorithm = hashing.Default())
			{
				return hashAlgorithm.ComputeHash(buffer).ComputeHex();
			}
		}

		public static string ComputeHex(Stream stream)
		{
			using (var hashAlgorithm = hashing.Default())
			{
				return hashAlgorithm.ComputeHex(stream);
			}
		}

		public static string ComputeHashString(string value)
		{
			using (var hashAlgorithm = hashing.Default())
			{
				return hashAlgorithm.ComputeHashString(value);
			}
		}

		public static string ComputeHex(string value)
		{
			using (var hashAlgorithm = hashing.Default())
			{
				return hashAlgorithm.ComputeHex(value);
			}
		}

		public static string ComputeHashBase64String(string value)
		{
			using (var hashAlgorithm = hashing.Default())
			{
				return hashAlgorithm.ComputeHashBase64String(value);
			}
		}

		public static void SetHashAlgorithm(Func<IHashAlgorithm> hashAlgorithm)
		{
			hashing.SetHashAlgorithm(hashAlgorithm);
		}

		public static void SetHashAlgorithmFactory(IHashAlgorithmFactory hashAlgorithmFactory)
		{
			hashing.SetHashAlgorithmFactory(hashAlgorithmFactory);
		}

		public static IHashAlgorithm Create(string providerName)
		{
			return hashing.Create(providerName);
		}

		public static void Register<T>() where T : IHashAlgorithm, new()
		{
			hashing.Register<T>();
		}

		public static void Register<T>(string providerName) where T : IHashAlgorithm, new()
		{
			hashing.Register<T>(providerName);
		}

		public static void Reset()
		{
			hashing.Reset();
		}
	}
}