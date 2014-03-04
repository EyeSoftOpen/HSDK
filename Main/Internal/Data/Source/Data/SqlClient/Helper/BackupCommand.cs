namespace EyeSoft.Data.SqlClient.Helper
{
	using System.Data.SqlClient;

	public class BackupCommand : HelperCommand
	{
		private readonly string databaseName;

		private readonly string backupPath;

		private readonly string backupDbCommand = "BACKUP DATABASE @databaseName TO DISK = @backupPath";

		public BackupCommand(SqlCommand command, string databaseName, string backupPath, bool appendDatabaseBackup)
			: base(command)
		{
			this.databaseName = databaseName;
			this.backupPath = backupPath;

			if (!appendDatabaseBackup)
			{
				backupDbCommand = string.Concat(backupDbCommand, " WITH INIT");
			}
		}

		public override void Execute()
		{
			using (command)
			{
				command.CommandText = backupDbCommand;

				command.Parameters.AddWithValue("@databaseName", databaseName);
				command.Parameters.AddWithValue("@backupPath", backupPath);

				command.ExecuteNonQuery();
			}
		}
	}
}