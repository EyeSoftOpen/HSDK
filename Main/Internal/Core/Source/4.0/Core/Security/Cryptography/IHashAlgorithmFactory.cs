namespace EyeSoft.Core.Security.Cryptography
{
	public interface IHashAlgorithmFactory
	{
		IHashAlgorithm Create(string providerName);
	}
}