namespace EyeSoft.Data.SqlClient
{
	using System;
	using System.Data.SqlClient;
	using System.Reflection;
    using EyeSoft.Reflection;
    using EyeSoft.Data.SqlClient.Helper;

    public class SqlDatabase : IDisposable
	{
		private readonly SqlConnection connection;

		public SqlDatabase(string connectionString)
			: this(new SqlConnection(connectionString))
		{
		}

		public SqlDatabase(SqlConnection connection)
		{
			this.connection = connection;

			connection.Open();
		}

		public void Backup(string databaseName, string backupPath, bool appendDatabaseBackup)
		{
			new BackupCommand(connection.CreateCommand(), databaseName, backupPath, appendDatabaseBackup)
				.Execute();
		}

		public void Restore(string databaseName, string backupPath, string restorePath)
		{
			new RestoreCommand(connection.CreateCommand(), databaseName, backupPath, restorePath)
				.Execute();
		}

		public void Restore(string databaseName, string backupPath)
		{
			new RestoreCommand(connection.CreateCommand(), databaseName, backupPath)
				.Execute();
		}

		public string[] GetDabaseNames()
		{
			return new DatabaseListCommand(connection.CreateCommand()).Execute();
		}

		public void ExecuteFromResource(string resourceName)
		{
			var commandText = Assembly.GetCallingAssembly().ReadResourceText(resourceName);

			Execute(commandText);
		}

		public void Execute(string commandText)
		{
			new GenericCommand(connection.CreateCommand(), commandText).Execute();
		}

		public void Dispose()
		{
			connection.Dispose();
		}
	}
}