namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System.Linq;

	public interface IDbContext
	{
		IQueryable<T> Table<T>() where T : class;

		int SaveChanges();
	}
}