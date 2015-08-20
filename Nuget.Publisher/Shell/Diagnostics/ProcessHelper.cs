namespace EyeSoft.Nuget.Publisher.Shell.Diagnostics
{
	using System.Diagnostics;

	public static class ProcessHelper
	{
		public static void Start(string fileName, string arguments = null, string workingDirectory = null)
		{
			var startInfo =
				new ProcessStartInfo
				{
					FileName = fileName,
					Arguments = arguments,
					WorkingDirectory = workingDirectory,
					UseShellExecute = false,
					CreateNoWindow = true,
					RedirectStandardError = true,
					RedirectStandardOutput = true
				};

			using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
			{
				process.ErrorDataReceived += Log;
				process.OutputDataReceived += Log;

				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();

				process.WaitForExit();
			}
		}

		private static void Log(object sender, DataReceivedEventArgs e)
		{
			if (e.Data == null)
			{
				return;
			}

			ConsoleHelper.WriteLine(e.Data);
		}
	}
}