namespace EyeSoft.Data.SqlClient.Backup
{
	public class SqlDatabasesBackup
	{
		private readonly BackupSettings settings;

		private readonly DatabaseBackupLogger logger;

		private readonly string[] databaseNames;

		public SqlDatabasesBackup(BackupSettings settings, DatabaseBackupLogger logger, string[] databaseNames)
		{
			this.settings = settings;
			this.logger = logger;
			this.databaseNames = databaseNames;
		}

		public int Run()
		{
			var backupFolder = settings.Folder;

			logger.Start();

			var systemConnectionString = settings.MasterConnection;

			var databaseBackupedCount =
				new SqlDatabases()
					.Backup(logger, systemConnectionString, backupFolder, databaseNames, settings.AppendDatabase);

			return databaseBackupedCount;
		}
	}
}