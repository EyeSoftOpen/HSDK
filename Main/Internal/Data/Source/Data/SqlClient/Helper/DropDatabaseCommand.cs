namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	public class DropDatabaseCommand : IHelperCommand
	{
		private const string DataBaseExistsSql =
			"IF EXISTS (SELECT name FROM sys.databases WHERE name = N'{0}')";

		private const string DropDataBaseIfExistSql =
			DataBaseExistsSql + "\n" +
			"ALTER DATABASE [{0}]" + "\n" +
			"SET SINGLE_USER" + "\n" +
			"WITH ROLLBACK IMMEDIATE" + "\n" +
			"USE Master" + "\n" +
			DataBaseExistsSql + "\n" +
			"DROP DATABASE [{0}]";

		private readonly string connectionString;

		private readonly string initialCatalog;

		public DropDatabaseCommand(string connectionString)
		{
			initialCatalog = SqlHelper.GetInitialCatalog(connectionString);
			this.connectionString = SqlHelper.GetMasterDatabaseConnection(connectionString);
		}

		public void Execute()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
				    var commandText = string.Format(DropDataBaseIfExistSql, initialCatalog);
					command.CommandText = commandText;

					connection.Open();

					command.ExecuteNonQuery();
				}
			}
		}
	}
}