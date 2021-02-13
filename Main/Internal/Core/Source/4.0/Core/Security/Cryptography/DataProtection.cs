namespace EyeSoft.Security.Cryptography
{
    using System.Security.Cryptography;
    using Extensions;

    public static class DataProtection
	{
		private const DataProtectionScope DefaultProtectionScope = DataProtectionScope.CurrentUser;

		public static byte[] Protect(this string data, DataProtectionScope dataProtectionScope = DefaultProtectionScope)
		{
			return data.ToByteArray().Protect(dataProtectionScope);
		}

		public static byte[] Unprotect(this string data, DataProtectionScope dataProptectionScope = DefaultProtectionScope)
		{
			return data.ToByteArray().Unprotect(dataProptectionScope);
		}

		public static string ProtectToString(this byte[] data, DataProtectionScope dataProtectionScope = DefaultProtectionScope)
		{
			return data.Protect(dataProtectionScope).GetString();
		}

		public static string UnprotectToString(this byte[] data, DataProtectionScope dataProptectionScope = DefaultProtectionScope)
		{
			return data.Unprotect(dataProptectionScope).GetString();
		}

		public static byte[] Protect(this byte[] data, DataProtectionScope dataProtectionScope = DefaultProtectionScope)
		{
			return ProtectedData.Protect(data, null, dataProtectionScope);
		}

		public static byte[] Unprotect(this byte[] data, DataProtectionScope dataProptectionScope = DefaultProtectionScope)
		{
			return ProtectedData.Unprotect(data, null, dataProptectionScope);
		}
	}
}
