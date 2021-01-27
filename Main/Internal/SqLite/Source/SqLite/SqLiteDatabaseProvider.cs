namespace EyeSoft.Data.SqLite
{
	using System.Data.SQLite;
	using System.IO;

	using EyeSoft.Data.Common;

	public class SqLiteDatabaseProvider
		: DatabaseProvider
	{
		private readonly SQLiteConnectionStringBuilder connectionStringBuilder;

		public SqLiteDatabaseProvider(SQLiteConnectionStringBuilder connectionStringBuilder)
			: base(connectionStringBuilder)
		{
			this.connectionStringBuilder = connectionStringBuilder;
		}

		public SqLiteDatabaseProvider(string connectionString)
			: base(connectionString)
		{
			connectionStringBuilder = new SQLiteConnectionStringBuilder(connectionString);
		}

		public override string ProviderName => DatabaseProviders.SqLite;

        public static void Register()
		{
			DatabaseProviders
				.Register<SQLiteConnectionStringBuilder>(
					DatabaseProviders.SqLite, c => new SqLiteDatabaseProvider(c));
		}

		public override bool Exists()
		{
			return File.Exists(connectionStringBuilder.DataSource);
		}

		public override void Create()
		{
		}

		public override void Drop()
		{
			File.Delete(connectionStringBuilder.DataSource);
		}
	}
}