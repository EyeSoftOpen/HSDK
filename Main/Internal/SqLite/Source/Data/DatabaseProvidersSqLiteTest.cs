namespace EyeSoft.Data.SqLite.Test
{
	using System.Data.SQLite;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqLite;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using FluentAssertions;

	[TestClass]
	public class DatabaseProvidersSqLiteTest
	{
		[TestMethod]
		public void VerifySqLiteConnectionStringBuilderGetRightDatabaseProvider()
		{
			SqLiteDatabaseProvider.Register();

            var databaseProvider = new SQLiteConnectionStringBuilder { DataSource = "Test.db" }.GetDatabaseProvider();

            databaseProvider.Should().BeOfType<SqLiteDatabaseProvider>();

            databaseProvider.ConnectionString.Should().Be("data source=Test.db");
		}
	}
}