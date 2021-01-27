namespace EyeSoft.Core.Security.Cryptography
{
    using System;
    using System.IO;

    public static class Hashing
	{
		private static readonly DefaultHashing hashing = new DefaultHashing();

		public static IHashAlgorithm Default
		{
			get { return hashing.Default(); }
		}

		public static IHashAlgorithm Md5
		{
			get { return hashing.Md5; }
		}

		public static IHashAlgorithm Sha1
		{
			get { return hashing.Sha1; }
		}

		public static IHashAlgorithm Sha256
		{
			get { return hashing.Sha256; }
		}

		public static IHashAlgorithm Sha384
		{
			get { return hashing.Sha384; }
		}

		public static IHashAlgorithm Sha512
		{
			get { return hashing.Sha512; }
		}

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