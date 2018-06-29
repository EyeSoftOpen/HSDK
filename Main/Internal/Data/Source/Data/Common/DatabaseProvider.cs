namespace EyeSoft.Data.Common
{
	using System;
	using System.Data;
	using System.Data.Common;

	public abstract class DatabaseProvider
		: IDatabaseProvider
	{
		protected DatabaseProvider(DbConnectionStringBuilder connectionStringBuilder)
			: this(connectionStringBuilder.ToString())
		{
		}

		protected DatabaseProvider(string connectionString)
		{
			ConnectionString = connectionString;
		}

		public string ConnectionString { get; private set; }

		public abstract string ProviderName { get; }

		public abstract bool Exists();

		public abstract void Create();

		public abstract void Drop();

		void IDatabaseProvider.ExecuteNonQuery(string statement, string connectionString)
		{
			this.ExecuteNonQuery(statement, connectionString);
		}

		T IDatabaseProvider.ExecuteScalar<T>(string statement, DbConnectionStringBuilder dbConnectionStringBuilder)
		{
			return this.ExecuteScalar<T>(statement, dbConnectionStringBuilder);
		}

		T IDatabaseProvider.ExecuteCommand<T>(string statement, string connectionString, Func<IDbCommand, T> func)
		{
			return this.ExecuteCommand<T>(statement, connectionString, func);
		}
	}
}