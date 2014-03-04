namespace EyeSoft.Shimmer.Client
{
	using System;
	using System.IO;
	using System.Linq;

	using EyeSoft.Diagnostic;
	using EyeSoft.Reflection;

	using global::Shimmer.Client;

	public class ShimmerConfiguration
	{
		private readonly string projectName;

		public ShimmerConfiguration(string applicationName, string url, string projectName = null, FrameworkVersion frameworkVersion = FrameworkVersion.Net45)
		{
			this.projectName = projectName;
			ApplicationName = applicationName;
			FrameworkVersion = frameworkVersion;

			UrlOrPath = !SystemInspector.Debugger.IsAttached ? CompleteUrl(url) : UrlOrDebugPath(url);
		}

		public string ApplicationName { get; private set; }

		public string UrlOrPath { get; private set; }

		public FrameworkVersion FrameworkVersion { get; private set; }

		protected internal virtual string DebugOrInstalledPath()
		{
			if (projectName == null)
			{
				const string Message =
					"If need to attach the debugger to the installed application provide the projectName" + "\r\n" +
					" or ovverride the method DebugOrInstalledPath.";

				throw new InvalidOperationException(Message);
			}

			var projectPath = SolutionSubFolderPath(projectName);

			var debugPath = Path.Combine(projectPath, "Bin", "Debug");

			return debugPath;
		}

		protected virtual string UrlOrDebugPath(string url)
		{
			var releasesFolder = SolutionSubFolderPath("RELEASES");

			return releasesFolder;
		}

		private string CompleteUrl(string url)
		{
			const string Http = "http://";

			const string UrlEnding = "/files";

			if (!url.StartsWith(Http, StringComparison.InvariantCultureIgnoreCase))
			{
				var completeUrl = string.Concat(Http, url, UrlEnding);
				return completeUrl;
			}

			if (url.EndsWith(UrlEnding))
			{
				throw new ArgumentException(string.Format("The full URL where the packages are located must end with '{0}'.", UrlEnding));
			}
			return url;
		}

		private string SolutionSubFolderPath(string solutionSubFolder)
		{
			var projectPath = AssemblyRuntime.GetCurrentWithoutDebug();

			var solutionDirectory = new DirectoryInfo(projectPath);

			while (!solutionDirectory.GetFiles("*.sln").Any())
			{
				solutionDirectory = solutionDirectory.Parent;
			}

			return Path.Combine(solutionDirectory.FullName, solutionSubFolder);
		}
	}
}