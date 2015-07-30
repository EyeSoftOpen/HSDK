namespace EyeSoft.Data.Test.SqlClient
{
	using System.Data.SqlClient;

	internal static class ConnectionString
	{
		public static string Get(string databaseName)
		{
			var connectionStringBuilder =
				new SqlConnectionStringBuilder
					{
						DataSource = ".",
						InitialCatalog = databaseName,
						IntegratedSecurity = true
					};

			return connectionStringBuilder.ConnectionString;
		}
	}
}