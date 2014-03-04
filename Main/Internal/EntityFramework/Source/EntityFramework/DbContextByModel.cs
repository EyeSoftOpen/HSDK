namespace EyeSoft.Data.EntityFramework
{
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;

	internal class DbContextByModel : DbContext
	{
		public DbContextByModel(string nameOrConnectionString, DbCompiledModel compiledModel)
			: base(nameOrConnectionString, compiledModel)
		{
		}

		public DbContextByModel(DbConnection connection, DbCompiledModel compiledModel)
			: base(connection, compiledModel, true)
		{
		}
	}
}