namespace EyeSoft.Core.Security.Cryptography
{
    using System;
    using System.IO;
    using System.Text;

    public static class HashAlgorithmExtensions
	{
		public static byte[] ComputeHash(this IHashAlgorithm hashAlgorithm, string value)
		{
			var hash = hashAlgorithm.ComputeHash(Encoding.Default.GetBytes(value));

			return hash;
		}

		public static string ComputeHashBase64String(this IHashAlgorithm hashAlgorithm, string value)
		{
			return Convert.ToBase64String(hashAlgorithm.ComputeHash(value));
		}

		public static string ComputeHashString(this IHashAlgorithm hashAlgorithm, string value)
		{
			return Encoding.Default.GetString(hashAlgorithm.ComputeHash(value));
		}

		public static string ComputeHexWithEncoding(this IHashAlgorithm hashAlgorithm, string value)
		{
			var hash = hashAlgorithm.ComputeHash(Encoding.Default.GetBytes(value));

			return hash.ComputeHex();
		}

		public static string ComputeHex(this IHashAlgorithm hashAlgorithm, string value)
		{
			var hash = hashAlgorithm.ComputeHash(value);

			return hash.ComputeHex();
		}

		public static string ComputeHex(this IHashAlgorithm hashAlgorithm, byte[] buffer)
		{
			var hash = hashAlgorithm.ComputeHash(buffer);

			return hash.ComputeHex();
		}

		public static string ComputeHex(this IHashAlgorithm hashAlgorithm, byte[] buffer, int offset, int count)
		{
			var hash = hashAlgorithm.ComputeHash(buffer, offset, count);

			return hash.ComputeHex();
		}

		public static string ComputeHex(this IHashAlgorithm hashAlgorithm, Stream stream)
		{
			var hash = hashAlgorithm.ComputeHash(stream);

			return ComputeHex(hash);
		}

		public static string ComputeHex(this byte[] hash)
		{
			var stringBuilder = new StringBuilder(40);

			foreach (var element in hash)
			{
				stringBuilder.Append(element.ToString("x2"));
			}

			return stringBuilder.ToString().ToLower();
		}
	}
}