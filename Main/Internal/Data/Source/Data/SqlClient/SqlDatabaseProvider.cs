namespace EyeSoft.Data.SqlClient
{
	using System.Data.SqlClient;

	using EyeSoft.Data.Common;
	using EyeSoft.Data.SqlClient.Helper;

	public class SqlDatabaseProvider
		: DatabaseProvider
	{
		public SqlDatabaseProvider(string connectionString)
			: base(connectionString)
		{
		}

		public SqlDatabaseProvider(SqlConnectionStringBuilder connectionBuilder)
			: base(connectionBuilder)
		{
		}

		public override string ProviderName
		{
			get { return DatabaseProviders.SqlServer; }
		}

		public override bool Exists()
		{
			return new DatabaseExistsCommand(ConnectionString).Execute();
		}

		public override void Create()
		{
			new CreateDatabaseCommand(ConnectionString).Execute();
		}

		public override void Drop()
		{
			new DropDatabaseCommand(ConnectionString).Execute();
		}
	}
}