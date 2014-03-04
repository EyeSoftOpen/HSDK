namespace EyeSoft.Data.Nhibernate.Test.Helpers
{
	using System.Data.Common;
	using System.Data.SqlClient;
	using System.Data.SQLite;
	using System.Data.SqlServerCe;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqLite;
	using EyeSoft.Testing.Data;

	internal static class DataProviderHelper
	{
		static DataProviderHelper()
		{
			SqlCeDatabaseProvider.Register();
			SqLiteDatabaseProvider.Register();
		}

		public static IDatabaseProvider Create(string dbName)
		{
			var sqLiteConnectionStringBuilder = GetSqLiteConnectionStringBuilder(dbName);

			var sqCeConnectionStringBuilder = GetSqlCeConnectionStringBuilder(dbName);

			var sqlConnectionStringBuilder = GetSqlConnectionStringBuilder(dbName);

			var databaseProvider = sqlConnectionStringBuilder.GetDatabaseProvider();

			return databaseProvider;
		}

		private static DbConnectionStringBuilder GetSqlCeConnectionStringBuilder(string dbName)
		{
			var sqlCeConnectionStringBuilder =
				new SqlCeConnectionStringBuilder
				{
					DataSource = dbName + ".sdf"
				};

			return sqlCeConnectionStringBuilder;
		}

		private static DbConnectionStringBuilder GetSqlConnectionStringBuilder(string dbName)
		{
			var sqlConnectionStringBuilder =
				new SqlConnectionStringBuilder(ConnectionString.Get(dbName));

			return sqlConnectionStringBuilder;
		}

		private static DbConnectionStringBuilder GetSqLiteConnectionStringBuilder(string dbName)
		{
			var sqLiteConnectionStringBuilder =
				new SQLiteConnectionStringBuilder
					{
						DataSource = dbName + ".db"
					};

			return sqLiteConnectionStringBuilder;
		}
	}
}