namespace EyeSoft.Logging
{
    using System;
    using System.IO;
    using IO;

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

			var erroLogFilePath = Path.Combine(currentLogPath, $"Error.Log.{dateTime.ToString("HH.mm.ss")}.xml");

			using (var stream = Storage.OpenWrite(erroLogFilePath))
			{
				new ExceptionXElement(exception).Save(stream);
			}
		}
	}
}