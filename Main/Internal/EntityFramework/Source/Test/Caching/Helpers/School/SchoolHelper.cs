namespace EyeSoft.Data.EntityFramework.Test.Caching.Helpers.School
{
	using System.Data.Entity;
	using System.Data.Entity.Infrastructure;
	using System.Data.Entity.ModelConfiguration.Conventions;

	using EyeSoft.Domain.Transactional;
	using EyeSoft.Testing.Data;
	using EyeSoft.Testing.Domain.Helpers.Domain1;

	internal static class SchoolHelper
	{
		private static readonly string connectionString = ConnectionString.Get("EntityFramework.School");

		private static readonly DbCompiledModel model = Build();

		public static void InitializeDb()
		{
			using (var entityFramework = CreateEntityFrameworkTransactionalCollection())
			{
				entityFramework.Drop();
				entityFramework.Create();
			}
		}

		public static SchoolUnitOfWork CreateUnitOfWork()
		{
			return new SchoolUnitOfWork(CreateEntityFrameworkTransactionalCollection());
		}

		public static ITransactionalCollection CreateCollection()
		{
			return CreateEntityFrameworkTransactionalCollection();
		}

		private static EntityFrameworkTransactionalCollection CreateEntityFrameworkTransactionalCollection()
		{
			return
				new EntityFrameworkTransactionalCollection(
					connectionString,
					model);
		}

		private static DbCompiledModel Build()
		{
			var modelBuilder = new DbModelBuilder();
			modelBuilder.Entity<School>();
			modelBuilder.Entity<Child>();
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

			var build = modelBuilder.Build(new DbProviderInfo("System.Data.SqlClient", "2008"));

			return build.Compile();
		}
	}
}