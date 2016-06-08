namespace EyeSoft.Nuget.Publisher.Core.Build
{
	using System;

	using EyeSoft.Nuget.Publisher.Core.Diagnostics;

	public class MsBuild
	{
		private readonly string projectPath;
		private readonly string solutionPath;
		private readonly bool logEnabled;

		public MsBuild(string projectPath, string solutionPath, bool logEnabled)
		{
			this.projectPath = projectPath;
			this.solutionPath = solutionPath;
			this.logEnabled = logEnabled;
		}

		public void Build()
		{
			Console.Write("Building the application...");

			const string FileName = @"C:\Program Files (x86)\MSBuild\14.0\Bin\msbuild.exe";
			var arguments = $"{projectPath} /p:Configuration=Release /p:DeployOnBuild=true /p:PublishProfile=Release /m";
			var workingDirectory = solutionPath;

			ProcessHelper.Start(FileName, arguments, workingDirectory, logEnabled);
		}
	}
}