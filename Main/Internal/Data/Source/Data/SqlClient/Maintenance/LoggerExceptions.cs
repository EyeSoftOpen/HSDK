namespace EyeSoft.Data.SqlClient.Maintenance
{
	using System;
	using System.IO;
    using Core.Extensions;
    using Core.Logging;

    public class FileExceptionLogger : ILogger
	{
		private readonly string folder;

		public FileExceptionLogger(string folder)
		{
			this.folder = folder;
		}

		public void Write(string message)
		{
			throw new NotImplementedException();
		}

		public void Error(Exception exception)
		{
			var lines =
				new[]
					{
						"========================================================",
                        $"WriteLog error at {DateTime.Now.FullTime()}",
						exception.Format()
					};

			File.AppendAllLines(Path.Combine(folder, "Log.Exceptions.txt"), lines);
		}
	}
}