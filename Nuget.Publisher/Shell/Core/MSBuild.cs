namespace EyeSoft.Nuget.Publisher.Shell
{
	using System;
	using System.Diagnostics;

	public static class MsBuild
	{
		public static void Build(string projectPath, string solutionFolder)
		{
			var startInfo =
				new ProcessStartInfo
					{
						FileName = @"C:\Program Files (x86)\MSBuild\12.0\Bin\msbuild.exe",
						Arguments = string.Format("{0} /p:Configuration=Release /m", projectPath),
						WorkingDirectory = solutionFolder,
						UseShellExecute = false,
						CreateNoWindow = true,
						RedirectStandardError = true,
						RedirectStandardOutput = true
					};

			Console.WriteLine("Building the application...");
			Console.WriteLine();

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

			Console.WriteLine(e.Data);
		}
	}
}