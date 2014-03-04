namespace EyeSoft.Data.SqlClient.Maintenance
{
	using System;
	using System.Collections.Generic;

	public class SqlDatabases
	{
		public int Restore(
			DatabaseLogger logger,
			string connectionString,
			IEnumerable<DbMaintenanceSettings> databasesToRestore)
		{
			var sqlDatabase = new SqlDatabase(connectionString);

			Action<string, string> action = sqlDatabase.Restore;

			return Run(logger, databasesToRestore, action);
		}

		public int Restore(
			DatabaseLogger logger,
			string connectionString,
			IEnumerable<DbMaintenanceSettings> databasesToRestore,
			string restorePath)
		{
			var sqlDatabase = new SqlDatabase(connectionString);

			Action<string, string> action =
				(databaseName, backupPath) => sqlDatabase.Restore(databaseName, backupPath, restorePath);

			return Run(logger, databasesToRestore, action);
		}

		public int Backup(
			DatabaseLogger logger,
			string connectionString,
			IEnumerable<DbMaintenanceSettings> databasesToBackup,
			bool appendDatabaseBackup)
		{
			var sqlDatabase = new SqlDatabase(connectionString);

			Action<string, string> action =
				(databaseName, databasePath) =>
					sqlDatabase.Backup(databaseName, databasePath, appendDatabaseBackup);

			return Run(logger, databasesToBackup, action);
		}

		private int Run(
			DatabaseLogger logger,
			IEnumerable<DbMaintenanceSettings> databasesToBackup,
			Action<string, string> maintenanceOperation)
		{
			var databaseBackupedCount = 0;

			foreach (var database in databasesToBackup)
			{
				try
				{
					maintenanceOperation(database.Name, database.Path);

					logger.Successed(database.Name);

					databaseBackupedCount++;
				}
				catch (Exception exception)
				{
					logger.Failed(database.Name, exception);
				}
			}

			return databaseBackupedCount;
		}
	}
}