namespace EyeSoft.Nuget.Publisher.Core.Diagnostics
{
	using System;
	using System.Diagnostics;

	using EyeSoft.Nuget.Publisher.Core.Core;

	public static class ProcessHelper
	{
		public static void Start(
			string fileName,
			string arguments = null,
			string workingDirectory = null,
			bool logEnabled = true,
			bool waitForExit = true)
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

			var stopwatch = Stopwatch.StartNew();

			using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
			{
				process.ErrorDataReceived += (s, e) => Log(logEnabled, s, e);
				process.OutputDataReceived += (s, e) => Log(logEnabled, s, e);

				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();

				if (waitForExit)
				{
					process.WaitForExit();
				}
			}

			stopwatch.Stop();

			Console.WriteLine(" in {0}", stopwatch.Elapsed.ToHumanReadable());
			Console.WriteLine();
		}

		private static void Log(bool logEnabled, object sender, DataReceivedEventArgs e)
		{
			if (!logEnabled)
			{
				return;
			}

			if (e.Data == null)
			{
				return;
			}

			Console.WriteLine(e.Data);
		}
	}
}