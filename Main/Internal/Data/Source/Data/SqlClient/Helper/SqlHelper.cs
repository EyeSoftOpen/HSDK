namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	internal static class SqlHelper
	{
		private const string MasterDatabase = "Master";

		public static string GetInitialCatalog(string connectionString)
		{
			var initialCatalog = new SqlConnectionStringBuilder(connectionString).InitialCatalog;

			return initialCatalog;
		}

		public static string GetMasterDatabaseConnection(string connectionString)
		{
			var masterConnectionString =
				new SqlConnectionStringBuilder(connectionString)
				{
					InitialCatalog = MasterDatabase
				}.ConnectionString;

			return masterConnectionString;
		}
	}
}
