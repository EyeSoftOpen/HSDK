namespace EyeSoft.Testing.Data
{
	using System.Data.SqlClient;

	public static class ConnectionString
	{
		public static string Get(string dbName, bool addPrefix = true)
		{
			var fullDbName = addPrefix ? string.Concat("EyeSoft.Hsdk.", dbName, ".Test") : dbName;

			return GetConnection(fullDbName);
		}

		public static string GetMaster()
		{
			return GetConnection();
		}

		private static string GetConnection(string fullDbName = null)
		{
			var sqlConnectionStringBuilder =
				new SqlConnectionStringBuilder
					{
						DataSource = @"(LocalDB)\V11.0",
						IntegratedSecurity = true,
					};

			if (!string.IsNullOrWhiteSpace(fullDbName))
			{
				sqlConnectionStringBuilder.InitialCatalog = fullDbName;
				sqlConnectionStringBuilder.ApplicationName = fullDbName;
			}

			return sqlConnectionStringBuilder.ToString();
		}
	}
}