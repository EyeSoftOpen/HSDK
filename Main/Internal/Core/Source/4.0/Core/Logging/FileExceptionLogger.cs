namespace EyeSoft.Logging
{
    using System;
    using System.IO;
    using IO;

    public class FileLogger : ILogger
	{
		private readonly string logFilePath;

		public FileLogger(string logFolder)
		{
			var dateTime = DateTime.Now;

			var currentLogPath = Path.Combine(logFolder, dateTime.Date.ToString(@"yyyy\\MM\\dd"));

			Storage.Directory(currentLogPath).Create();

			logFilePath = Path.Combine(currentLogPath, $"Log.{dateTime.ToString("HH.mm.ss")}.txt");
		}

		public void Write(string message)
		{
			File.AppendAllLines(logFilePath, new[] { message });
		}

		public void Error(Exception exception)
		{
		}
	}
}