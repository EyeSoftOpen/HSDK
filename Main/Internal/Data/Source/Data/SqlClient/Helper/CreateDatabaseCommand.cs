namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data;
	using System.Data.SqlClient;

	public class CreateDatabaseCommand : IHelperCommand
	{
		private const string CreateDataBaseSql = "CREATE DATABASE [{DbName}]";

		private readonly string connectionString;

		private readonly string initialCatalog;

		public CreateDatabaseCommand(string connectionString)
		{
			initialCatalog = SqlHelper.GetInitialCatalog(connectionString);

			this.connectionString = SqlHelper.GetMasterDatabaseConnection(connectionString);

			if (new DatabaseExistsCommand(connectionString).Execute())
			{
				new DataException($"The database {initialCatalog} already exists.")
					.Throw();
			}
		}

		public void Execute()
		{
			using (var connection = new SqlConnection(connectionString))
			{
				using (var command = connection.CreateCommand())
				{
					var commandText = $"CREATE DATABASE[ {initialCatalog}]";

					command.CommandText = commandText;

					connection.Open();

					command.ExecuteNonQuery();
				}
			}
		}
	}
}