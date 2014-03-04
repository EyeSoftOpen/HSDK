namespace EyeSoft.Data.Nhibernate.Test.Helpers
{
	using System.Data.SqlServerCe;
	using System.IO;

	using EyeSoft;
	using EyeSoft.Data.Common;

	internal class SqlCeDatabaseProvider : DatabaseProvider
	{
		private readonly SqlCeConnectionStringBuilder connectionStringBuilder;

		public SqlCeDatabaseProvider(SqlCeConnectionStringBuilder connectionStringBuilder)
			: base(connectionStringBuilder)
		{
			Enforce.Argument(() => connectionStringBuilder);

			this.connectionStringBuilder = connectionStringBuilder;
		}

		public SqlCeDatabaseProvider(string connectionString)
			: base(connectionString)
		{
			Enforce.Argument(() => connectionString);

			connectionStringBuilder = new SqlCeConnectionStringBuilder(connectionString);
		}

		public override string ProviderName
		{
			get
			{
				return DatabaseProviders.SqlCe40;
			}
		}

		public static void Register()
		{
			DatabaseProviders.Register<SqlCeConnectionStringBuilder>(DatabaseProviders.SqlCe40, c => new SqlCeDatabaseProvider(c));
		}

		public override bool Exists()
		{
			return File.Exists(connectionStringBuilder.DataSource);
		}

		public override void Create()
		{
			new SqlCeEngine(connectionStringBuilder.ConnectionString).CreateDatabase();
		}

		public override void Drop()
		{
			File.Delete(connectionStringBuilder.DataSource);
		}
	}
}