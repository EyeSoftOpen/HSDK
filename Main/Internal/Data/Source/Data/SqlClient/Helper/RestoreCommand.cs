namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;
	using System.IO;
    using Base;

    public class RestoreCommand : HelperCommand
	{
		private readonly string databaseName;

		private readonly string backupPath;

		private string backupDbCommand =
			"RESTORE DATABASE @databaseName FROM DISK = @backupPath";

		public RestoreCommand(SqlCommand command, string databaseName, string backupPath)
			: this(command, databaseName, backupPath, null)
		{
		}

		public RestoreCommand(SqlCommand command, string databaseName, string backupPath, string restorePath)
			: base(command)
		{
			this.databaseName = databaseName;
			this.backupPath = backupPath;

			if (restorePath != null)
			{
				AddWithMoveClause(restorePath);
			}

			command.CommandText = backupDbCommand;
		}

		public override void Execute()
		{
			using (command)
			{
				command.Parameters.AddWithValue("@databaseName", databaseName);
				command.Parameters.AddWithValue("@backupPath", backupPath);

				command.ExecuteNonQuery();
			}
		}

		private void AddWithMoveClause(string restorePath)
		{
			const string WithMoveClause =
				"WITH MOVE @databaseName TO @restoreDbFilePath,\r\n" +
				"MOVE @databaseLogName TO @restoreDbLogFilePath";

			var restoreDbFilePath = Path.ChangeExtension(Path.Combine(restorePath, databaseName), ".mdf");
			var restoreDbLogFilePath = Path.ChangeExtension(Path.Combine(restorePath, databaseName), ".ldf");

			command.Parameters.AddWithValue("@databaseLogName", string.Format("{0}_log", databaseName));
			command.Parameters.AddWithValue("@restoreDbFilePath", restoreDbFilePath);
			command.Parameters.AddWithValue("@restoreDbLogFilePath", restoreDbLogFilePath);

			backupDbCommand = string.Concat(backupDbCommand, WithMoveClause);
		}
	}
}