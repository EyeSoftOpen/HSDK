namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;

	public class DatabaseExistsCommand : IHelperCommand<bool>
	{
		private const string CheckDataBaseExistsSql =
			"SELECT" + "\n" +
			"	(CASE" +
			"		WHEN EXISTS(" +
			"			SELECT NULL AS [EMPTY]" +
			"			FROM [sys].[databases] AS [t0]" +
			"			WHERE [t0].[name] = N'{DbName}'" +
			"			) THEN 1" +
			"		ELSE 0" +
			"	 END) AS [value]";

		private readonly string connectionString;

		private readonly string initialCatalog;

		public DatabaseExistsCommand(string connectionString)
		{
			initialCatalog = SqlHelper.GetInitialCatalog(connectionString);
			this.connectionString = SqlHelper.GetMasterDatabaseConnection(connectionString);
		}

		public bool Execute()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					var commandText = CheckDataBaseExistsSql.NamedFormat(initialCatalog);

					command.CommandText = commandText;

					connection.Open();

					var exists = command.ExecuteScalar<bool>();

					return exists;
				}
			}
		}
	}
}