namespace EyeSoft.Data.EntityFramework.Caching
{
	using System.Data.Entity.Config;

	public class CachedDbConfiguration : DbConfiguration
	{
		public CachedDbConfiguration(IEntityFrameworkCache entityFrameworkCache)
		{
			SetDefaultConnectionFactory(new CachedContextConnectionFactory(entityFrameworkCache));
		}
	}
}