namespace EyeSoft.Data.SqlClient.Maintenance
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
    using Core.Extensions;

    public class DatabaseLogger
	{
		private readonly List<string> log = new List<string>();

		private readonly string operationName;

		public DatabaseLogger(DbMaintenanceOperationType maintenanceOperationType)
		{
			operationName = maintenanceOperationType.ToString().ToLower();
		}

		public bool HasErrors { get; private set; }

		public void Start()
		{
			AppendLineSeparator();
			Append("Started {0} at {1}.", operationName, FullTime());
		}

		public void End(int databaseBackupedCount, int databaseToBackupCount)
		{
			const string Message = "Ended {0} at {1}. Copied {2} of {3} databases.";
			Append(Message, operationName, FullTime(), databaseBackupedCount, databaseToBackupCount);
			AppendLineSeparator();
		}

		public void Successed(string databaseName)
		{
			Append("Database success {0}, at {1}.", databaseName, FullTime());
		}

		public void Failed(string databaseName, Exception exception)
		{
			HasErrors = true;

			Append("Database failed {0}, at time: {1}. {2}", databaseName, FullTime(), exception.Format());
		}

		public void Failed(Exception exception)
		{
			HasErrors = true;

			Append("Error before start {0}, message: {1}", operationName, exception.Format());
		}

		public override string ToString()
		{
			var allLogs =
				log.Aggregate((previous, current) => string.Format("{0}{1}{2}", previous, Environment.NewLine, current));

			return string.Format("{0} {1}", allLogs, Environment.NewLine);
		}

		private void AppendLineSeparator()
		{
			Append("==========================================================================");
		}

		private void Append(string format, params object[] args)
		{
			log.Add(string.Format(format.Trim(), args));
		}

		private string FullTime()
		{
			return DateTime.Now.FullTime();
		}
	}
}