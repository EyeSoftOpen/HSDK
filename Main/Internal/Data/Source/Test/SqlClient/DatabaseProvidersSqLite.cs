namespace EyeSoft.Data.Test.SqlClient
{
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqlClient;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	[TestClass]
	public class DatabaseProvidersSqLite
	{
		[TestMethod]
		public void VerifySqLiteConnectionStringBuilderGetRightDatabaseProvider()
        {
            var databaseProvider = new SqlConnectionStringBuilder { DataSource = "Test.db" }.GetDatabaseProvider();

            databaseProvider.Should().BeOfType<SqlDatabaseProvider>();

            databaseProvider.ConnectionString.Should().Be("Data Source=Test.db");
        }
	}
}