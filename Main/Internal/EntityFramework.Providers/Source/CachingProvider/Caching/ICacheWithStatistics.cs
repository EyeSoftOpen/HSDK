namespace EyeSoft.Data.EntityFramework.Caching
{
	public interface ICacheWithStatistics : IEntityFrameworkCache
	{
		int Hits { get; }

		int Misses { get; }

		int ItemAdds { get; }

		int ItemInvalidations { get; }
	}
}