namespace EyeSoft.Windows.Runtime
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Linq;

	public class ApplicationRuntimeConfiguration
	{
		public ApplicationRuntimeConfiguration(string companyName, string applicationName, string downloadUrl, string projectPath, params string[] subFolders)
		{
			if (companyName == null)
			{
				throw new ArgumentNullException("companyName");
			}

			if (applicationName == null)
			{
				throw new ArgumentNullException("applicationName");
			}

			if (downloadUrl == null)
			{
				throw new ArgumentNullException("downloadUrl");
			}

			if (projectPath == null)
			{
				throw new ArgumentNullException("projectPath");
			}

			CompanyName = companyName;
			ApplicationName = applicationName;

			DownloadUrl = downloadUrl;
			ProjectPath = projectPath;

			subFolders = subFolders ?? Enumerable.Empty<string>().ToArray();
			SubFolders = new ReadOnlyCollection<string>(subFolders);

			DebugPath = ApplicationRuntime.DebugPath(projectPath);
			RuntimePath = ApplicationRuntime.RuntimePath(companyName, applicationName, subFolders);
			DebugOrRuntimePath = ApplicationRuntime.DebugOrRuntimePath(companyName, applicationName, projectPath, subFolders);
		}

		public string CompanyName { get; private set; }

		public string ApplicationName { get; private set; }

		public string DownloadUrl { get; private set; }

		public string DebugPath { get; private set; }

		public string RuntimePath { get; private set; }

		public string DebugOrRuntimePath { get; private set; }

		public string ProjectPath { get; private set; }

		public IEnumerable<string> SubFolders { get; private set; }
	}
}