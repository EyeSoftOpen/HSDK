namespace EyeSoft.Data.Test.SqlClient
{
	using EyeSoft.Data.SqlClient;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class SqlClientDatabaseProviderTest
	{
		[TestMethod]
		public void CreateDatabase()
		{
			var connectionBuilder = ConnectionString.Get("SqlClient");

			var databaseProvider = new SqlDatabaseProvider(connectionBuilder);

			if (databaseProvider.Exists())
			{
				Executing
					.This(databaseProvider.Drop)
					.Should("The database drop doesn't work.").NotThrow();
			}

			Executing
				.This(databaseProvider.Create)
				.Should("The database creation doesn't work.").NotThrow();

			databaseProvider
				.Exists()
				.Should("The database was not created.").Be.True();
		}
	}
}