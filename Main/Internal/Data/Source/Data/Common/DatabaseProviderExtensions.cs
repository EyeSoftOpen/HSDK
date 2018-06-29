namespace EyeSoft.Data.Common
{
	using System;
	using System.Data;
	using System.Data.Common;

	using EyeSoft.Extensions;

	public static class DatabaseProviderExtensions
	{
		public static void CreateIfNotExists(this IDatabaseProvider databaseProvider)
		{
			if (!databaseProvider.Exists())
			{
				databaseProvider.Create();
			}
		}

		public static void DropIfExists(this IDatabaseProvider databaseProvider)
		{
			if (databaseProvider.Exists())
			{
				databaseProvider.Drop();
			}
		}

		public static void DropIfExistsAndCreate(this IDatabaseProvider databaseProvider)
		{
			if (databaseProvider.Exists())
			{
				databaseProvider.Drop();
			}

			databaseProvider.Create();
		}

		public static void ExecuteNonQuery(
			this IDatabaseProvider databaseProvider,
			string statement,
			DbConnectionStringBuilder dbConnectionBuilder = null)
		{
			var connectionString = (dbConnectionBuilder == null) ? null : dbConnectionBuilder.ToString();

			ExecuteNonQuery(
				databaseProvider,
				statement,
				connectionString);
		}

		public static void ExecuteNonQuery(
			this IDatabaseProvider databaseProvider,
			string statement,
			string connectionString = null)
		{
			ExecuteCommand(
				databaseProvider,
				statement,
				connectionString,
				command => command.ExecuteNonQuery());
		}

		public static T ExecuteScalar<T>(
			this IDatabaseProvider databaseProvider,
			string statement,
			DbConnectionStringBuilder dbConnectionBuilder = null)
		{
			var connectionString = (dbConnectionBuilder == null) ? null : dbConnectionBuilder.ToString();

			return
				ExecuteScalar<T>(
					databaseProvider,
					statement,
					connectionString);
		}

		public static T ExecuteScalar<T>(
			this IDatabaseProvider databaseProvider,
			string statement,
			string connectionString = null)
		{
			return
				ExecuteCommand(
					databaseProvider,
					statement,
					connectionString,
					command => command.ExecuteScalar().Convert<T>());
		}

		public static T ExecuteCommand<T>(
			this IDatabaseProvider databaseProvider,
			string statement,
			string connectionString,
			Func<IDbCommand, T> func)
		{
			var factory = DbProviderFactories.GetFactory(databaseProvider.ProviderName);

			using (var connection = factory.CreateConnection())
			{
				connection.ConnectionString =
					connectionString ?? databaseProvider.ConnectionString;

				using (var command = connection.CreateCommand())
				{
					connection.Open();
					command.CommandText = statement;
					return func(command);
				}
			}
		}
	}
}