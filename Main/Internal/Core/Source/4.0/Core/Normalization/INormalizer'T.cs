namespace EyeSoft.Core.Normalization
{
	public interface INormalizer<in T> : INormalizer where T : class
	{
		void Normalize(T obj);
	}
}