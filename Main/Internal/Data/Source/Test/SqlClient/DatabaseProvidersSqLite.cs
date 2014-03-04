namespace EyeSoft.Data.Test.SqlClient
{
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqlClient;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class DatabaseProvidersSqLite
	{
		[TestMethod]
		public void VerifySqLiteConnectionStringBuilderGetRightDatabaseProvider()
		{
			new SqlConnectionStringBuilder { DataSource = "Test.db" }
				.GetDatabaseProvider()
				.Should().Be.InstanceOf<SqlDatabaseProvider>()
				.And.Value.ConnectionString.Should().Be.EqualTo("Data Source=Test.db");
		}
	}
}