namespace EyeSoft.EntityFramework.Caching.Demo.Domain
{
	using System;
	using System.Data.Common;
	using System.Data.Entity;
	using System.Data.Entity.ModelConfiguration;
	using System.Linq;
	using System.Reflection;

	using EyeSoft.EntityFramework.Caching.Demo.Domain.Mapping;

	public class NorthwindContext : DbContext, IDbContext
	{
		static NorthwindContext()
		{
			Database.SetInitializer<NorthwindContext>(null);
		}

		public NorthwindContext(string nameOrConnectionString)
			: base(nameOrConnectionString)
		{
		}

		public NorthwindContext(DbConnection dbConnection)
			: base(dbConnection, true)
		{
		}

		public IQueryable<T> Table<T>() where T : class
		{
			return Set<T>();
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			var configType = typeof(CategoryMap);

			var typesToRegister =
				Assembly.GetAssembly(configType).GetTypes()
					.Where(type => !string.IsNullOrEmpty(type.Namespace))
					.Where(IsTypeToMap);

			var configurationInstances =
				typesToRegister.Select(Activator.CreateInstance);

			foreach (var configurationInstance in configurationInstances)
			{
				modelBuilder.Configurations.Add((dynamic)configurationInstance);
			}
		}

		private static bool IsTypeToMap(Type type)
		{
			return
				type.BaseType != null &&
				type.BaseType.IsGenericType &&
				type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>);
		}
	}
}