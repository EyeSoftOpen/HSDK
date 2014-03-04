namespace EyeSoft.Logging
{
	using System;
	using System.IO;

	using EyeSoft.IO;

	public class XmlFileExceptionLogger : ILogger
	{
		private readonly string logFolder;

		public XmlFileExceptionLogger(string logFolder)
		{
			this.logFolder = logFolder;
		}

		public void Write(string message)
		{
		}

		public void Error(Exception exception)
		{
			var dateTime = DateTime.Now;

			var currentLogPath = Path.Combine(logFolder, dateTime.Date.ToString(@"yyyy\\MM\\dd"));

			Storage.Directory(currentLogPath).Create();

			var logFilePath = Path.Combine(currentLogPath, string.Format("Log.{0}.xml", dateTime.ToString("HH.mm.ss")));

			using (var stream = Storage.OpenWrite(logFilePath))
			{
				new ExceptionXElement(exception).Save(stream);
			}
		}
	}
}