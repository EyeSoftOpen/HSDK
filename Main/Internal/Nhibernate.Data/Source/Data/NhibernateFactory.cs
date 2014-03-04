namespace EyeSoft.Data.Nhibernate
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;
	using EyeSoft.Domain.Transactional;

	public static class NhibernateFactory
	{
		private static readonly IDictionary<Type, NhibernateConfiguration> configurations =
			new Dictionary<Type, NhibernateConfiguration>();

		public static T Create<T>(SchemaAction schemaAction = SchemaAction.None)
			where T : UnitOfWork
		{
			return CreateByDatabaseProvider<T>(DefaultDataBaseProvider<T>(), schemaAction);
		}

		public static T CreateByDatabaseProvider<T>(IDatabaseProvider databaseProvider, SchemaAction schemaAction = SchemaAction.None)
			where T : UnitOfWork
		{
			return
				(T)InitUnitOfWork<T>().UnitOfWork(databaseProvider, schemaAction);
		}

		public static T CreateByConnectionStringBuilder<T>(
			DbConnectionStringBuilder connectionStringBuilder,
			SchemaAction schemaAction = SchemaAction.None)
			where T : UnitOfWork
		{
			return CreateByDatabaseProvider<T>(connectionStringBuilder.GetDatabaseProvider(), schemaAction);
		}

		public static IDatabaseProvider DatabaseProvider<T>() where T : UnitOfWork
		{
			return DefaultDataBaseProvider<T>();
		}

		public static NhibernateConfiguration Configuration<T>() where T : UnitOfWork
		{
			return InitUnitOfWork<T>();
		}

		private static NhibernateConfiguration InitUnitOfWork<T>() where T : UnitOfWork
		{
			var unitOfWorkType = typeof(T);

			if (!configurations.ContainsKey(unitOfWorkType))
			{
				var configuration = new NhibernateConfiguration(unitOfWorkType);

				configurations.Add(unitOfWorkType, configuration);
			}

			return configurations[unitOfWorkType];
		}

		private static IDatabaseProvider DefaultDataBaseProvider<T>()
			where T : UnitOfWork
		{
			var unitOfWorkType = typeof(T);

			var dbName =
				unitOfWorkType.Name.Replace("UnitOfWork", null);

			var connectionStringBuilder =
				new SqlConnectionStringBuilder
				{
					DataSource = @".",
					InitialCatalog = dbName,
					IntegratedSecurity = true,
					Pooling = false
				};

			return connectionStringBuilder.GetDatabaseProvider();
		}
	}
}