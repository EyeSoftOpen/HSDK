namespace EyeSoft.Security.Cryptography
{
	public interface IHashAlgorithmFactory
	{
		IHashAlgorithm Create(string providerName);
	}
}