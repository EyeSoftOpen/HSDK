namespace EyeSoft.Data.SqLite.Test
{
	using System.Data.SQLite;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqLite;

	using Microsoft.VisualStudio.TestTools.UnitTesting;

	using SharpTestsEx;

	[TestClass]
	public class DatabaseProvidersSqLiteTest
	{
		[TestMethod]
		public void VerifySqLiteConnectionStringBuilderGetRightDatabaseProvider()
		{
			SqLiteDatabaseProvider.Register();

			new SQLiteConnectionStringBuilder { DataSource = "Test.db" }
				.GetDatabaseProvider()
				.Should().Be.InstanceOf<SqLiteDatabaseProvider>()
				.And.Value.ConnectionString.Should().Be.EqualTo("data source=Test.db");
		}
	}
}