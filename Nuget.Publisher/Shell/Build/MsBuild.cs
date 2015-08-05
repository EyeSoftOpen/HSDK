namespace EyeSoft.Nuget.Publisher.Shell.Build
{
	using System;
	using System.Diagnostics;
	using System.IO;

	public static class MsBuild
	{
		public static void Build(string projectPath)
		{
			Build(projectPath, new FileInfo(projectPath).Directory.FullName);
		}

		public static void Build(string projectPath, string solutionFolder)
		{
			var startInfo =
				new ProcessStartInfo
					{
						FileName = @"C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe",
						Arguments = $"{projectPath} /p:Configuration=Release /m",
						WorkingDirectory = solutionFolder,
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardError = true,
						RedirectStandardOutput = true
					};

			ConsoleHelper.WriteLine();
			Console.Write("Building the application...");

			var stopwatch = Stopwatch.StartNew();

			using (var process = new Process { StartInfo = startInfo, EnableRaisingEvents = true })
			{
				process.ErrorDataReceived += Log;
				process.OutputDataReceived += Log;

				process.Start();
				process.BeginErrorReadLine();
				process.BeginOutputReadLine();

				process.WaitForExit();
			}

			stopwatch.Stop();

			ConsoleHelper.WriteLine(" in {0}", stopwatch.Elapsed.ToHumanReadable());
			ConsoleHelper.WriteLine();
		}

		private static void Log(object sender, DataReceivedEventArgs e)
		{
			////if (e.Data == null)
			////{
			////	return;
			////}

			////ConsoleHelper.WriteLine(e.Data);
		}
	}
}