namespace EyeSoft.Core.Security.Cryptography
{
    using System.Collections.Generic;

    public static class HashAlgorithms
	{
		public const string Md5 = "MD5";
		public const string Sha1 = "Sha1";
		public const string Sha256 = "Sha256";
		public const string Sha384 = "Sha384";
		public const string Sha512 = "Sha512";

		public static readonly IEnumerable<string> All = new[] { Md5, Sha1, Sha256, Sha384, Sha512 };
	}
}