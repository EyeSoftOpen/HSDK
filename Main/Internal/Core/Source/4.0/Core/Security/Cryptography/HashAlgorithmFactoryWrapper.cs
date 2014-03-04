namespace EyeSoft.Security.Cryptography
{
	using System.Security.Cryptography;

	public class HashAlgorithmFactoryWrapper : IHashAlgorithmFactory
	{
		public IHashAlgorithm Create(string providerName)
		{
			Enforce.Argument(() => providerName);

			return new HashAlgorithmWrapper(HashAlgorithm.Create(providerName));
		}
	}
}