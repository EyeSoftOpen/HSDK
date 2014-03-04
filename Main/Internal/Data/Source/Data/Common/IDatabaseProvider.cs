namespace EyeSoft.Data.Common
{
	using System;
	using System.Data;
	using System.Data.Common;

	public interface IDatabaseProvider
	{
		string ConnectionString { get; }

		string ProviderName { get; }

		bool Exists();

		void Create();

		void Drop();

		void ExecuteNonQuery(string statement, string connectionString);

		T ExecuteScalar<T>(string statement, DbConnectionStringBuilder dbConnectionStringBuilder);

		T ExecuteCommand<T>(string statement, string connectionString, Func<IDbCommand, T> func);
	}
}