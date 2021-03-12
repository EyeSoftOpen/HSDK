namespace EyeSoft.Data.Test.SqlClient
{
    using System;
    using EyeSoft.Data.SqlClient;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	////[TestClass]: Integration test
	public class SqlClientDatabaseProviderTest
	{
		public void CreateDatabase()
		{
			var connectionBuilder = ConnectionString.Get("SqlClient");

			var databaseProvider = new SqlDatabaseProvider(connectionBuilder);

			if (databaseProvider.Exists())
			{
                Action action = databaseProvider.Drop;

				action.Should().NotThrow("The database drop doesn't work.");
			}

            Action create = databaseProvider.Create;
            create.Should().NotThrow("The database creation doesn't work.");

            databaseProvider.Exists().Should().BeTrue("The database was not created.");
		}
	}
}