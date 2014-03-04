namespace EyeSoft.Normalization
{
	public abstract class Normalizer<T> : INormalizer<T> where T : class
	{
		public abstract void Normalize(T obj);

		void INormalizer.Normalize(object obj)
		{
			Normalize((T)obj);
		}
	}
}