namespace EyeSoft.Data.Common
{
	using System;
	using System.Collections.Generic;
	using System.Data.Common;
	using System.Data.SqlClient;
    using Core.Extensions;
    using EyeSoft.Data.SqlClient;

    public static class DatabaseProviders
	{
		public const string SqlServer = "System.Data.SqlClient";
		public const string SqLite = "System.Data.SQLite";
		public const string SqlCe40 = "System.Data.SqlServerCe.4.0";
		public const string MySql = "MySql.Data.MySQLClient";
		public const string MySql5 = "MySql-5.0";

		private static readonly IDictionary<string, Delegate> databaseProviderDictionary =
			new Dictionary<string, Delegate>();

		private static readonly IDictionary<string, string> connectionStringBuilderProviderNameDictionary =
			new Dictionary<string, string>();

		static DatabaseProviders()
		{
			Register<SqlConnectionStringBuilder>(SqlServer, c => new SqlDatabaseProvider(c));
		}

		public static void Register<T>(
			string providerName,
			Func<T, IDatabaseProvider> databaseProviderBuilder)
			where T : DbConnectionStringBuilder
		{
			if (databaseProviderDictionary.ContainsKey(providerName))
			{
				return;
			}

			var providerBuilder = databaseProviderBuilder.Convert<Delegate>();
			databaseProviderDictionary.Add(providerName, providerBuilder);

			var connectionStringBuilderName = typeof(T).FullName;
			connectionStringBuilderProviderNameDictionary.Add(connectionStringBuilderName, providerName);
		}

		public static IDatabaseProvider GetDatabaseProvider(this DbConnectionStringBuilder connectionStringBuilder)
		{
			var connectionStringBuilderTypeName = connectionStringBuilder.GetType().FullName;

			if (!connectionStringBuilderProviderNameDictionary.ContainsKey(connectionStringBuilderTypeName))
			{
				var message = $"The provider {connectionStringBuilderTypeName} is not known. Use the RegisterDatabaseProvider method.";

				throw new ArgumentException(message);
			}

			var providerName = connectionStringBuilderProviderNameDictionary[connectionStringBuilderTypeName];

			return
				databaseProviderDictionary[providerName]
					.DynamicInvoke(connectionStringBuilder)
					.Convert<IDatabaseProvider>();
		}
	}
}