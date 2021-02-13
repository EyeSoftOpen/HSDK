namespace EyeSoft.Windows.Runtime
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;

	using EyeSoft.Diagnostic;
	using EyeSoft.IO;
	using EyeSoft.Reflection;

	public static class ApplicationRuntime
	{
		private const InstallationType DefaultInstallationType = InstallationType.LocalUser;

		private static readonly IDictionary<InstallationType, Environment.SpecialFolder> installationToPath =
			new Dictionary<InstallationType, Environment.SpecialFolder>
				{
					{ InstallationType.LocalUser, Environment.SpecialFolder.LocalApplicationData },
					{ InstallationType.ProgramFilesX86, Environment.SpecialFolder.ProgramFilesX86 },
					{ InstallationType.ProgramFiles, Environment.SpecialFolder.ProgramFiles }
				};

		public static IApplicationRuntimeConfigurationName Name(string company, string applicationName, params string[] subFolders)
		{
			return Name(company, applicationName, DefaultInstallationType, subFolders);
		}

		public static IApplicationRuntimeConfigurationName Name(string company, string applicationName, InstallationType installationType, params string[] subFolders)
		{
			return new FluentApplicationRuntimeConfiguration(company, applicationName, installationType, subFolders);
		}

		public static string DebugOrRuntimePath(string company, string applicationName, string projectName, params string[] subFolders)
		{
			return DebugOrRuntimePath(company, applicationName, projectName, DefaultInstallationType, subFolders);
		}

		public static string DebugOrRuntimePath(string company, string applicationName, string projectName, InstallationType programFilesX86, params string[] subFolders)
		{
			var isAttached = SystemInspector.Debugger.IsAttached;

			return isAttached ? DebugPath(projectName) : RuntimePath(company, applicationName, subFolders);
		}

		public static string RuntimePath(string company, string applicationName, params string[] subFolders)
		{
			return RuntimePath(company, applicationName, DefaultInstallationType, subFolders);
		}

		public static string RuntimePath(
			string company,
			string applicationName,
			InstallationType installationType,
			params string[] subFolders)
		{
			subFolders = subFolders ?? Enumerable.Empty<string>().ToArray();

			var localApplicationData = Environment.GetFolderPath(installationToPath[installationType]);

			var applicationWithSubFolders = new[] { localApplicationData, company, applicationName }.Union(subFolders).ToArray();

			var applicationPath = Path.Combine(applicationWithSubFolders);

			return applicationPath;
		}

		public static string DebugPath(string projectName)
		{
			if (projectName == null)
			{
				const string Message =
					"If need to attach the debugger to the installed application provide the projectName" + "\r\n" +
					" or ovverride the method DebugOrInstalledPath.";

				throw new InvalidOperationException(Message);
			}

			var projectPath = SolutionSubFolderPath(projectName);

			if (projectPath == null)
			{
				return null;
			}

			var debugOrRelease = Storage.File(System.Reflection.Assembly.GetEntryAssembly().Location).Directory.Name;

			var debugPath = Path.Combine(projectPath, "Bin", debugOrRelease);

			return debugPath;
		}

		private static string SolutionSubFolderPath(string solutionSubFolder)
		{
			var projectPath = AssemblyRuntime.GetCurrentWithoutDebug();

			var solutionDirectory = Storage.Directory(projectPath);

			while (!solutionDirectory.GetFiles("*.sln").Any())
			{
				solutionDirectory = solutionDirectory.Parent;

				if (solutionDirectory == null)
				{
					break;
				}
			}

			var path = solutionDirectory == null ? null : Path.Combine(solutionDirectory.FullName, solutionSubFolder);

			return path;
		}
	}
}