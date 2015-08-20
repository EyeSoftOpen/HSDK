namespace EyeSoft.Nuget.Publisher.Shell.Build
{
	using System;
	using System.Diagnostics;
	using System.IO;

	using EyeSoft.Nuget.Publisher.Shell.Diagnostics;

	public static class MsBuild
	{
		public static void Build(string projectPath)
		{
			Build(projectPath, new FileInfo(projectPath).Directory.FullName);
		}

		public static void Build(string projectPath, string solutionFolder)
		{
			const string MsBuildExe = @"C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe";
			var arguments = $"{projectPath} /p:Configuration=Release /m";

			ConsoleHelper.WriteLine();
			Console.Write("Building the application...");

			var stopwatch = Stopwatch.StartNew();

			ProcessHelper.Start(MsBuildExe, arguments, solutionFolder);

			stopwatch.Stop();

			ConsoleHelper.WriteLine(" in {0}", stopwatch.Elapsed.ToHumanReadable());
			ConsoleHelper.WriteLine();
		}
	}
}