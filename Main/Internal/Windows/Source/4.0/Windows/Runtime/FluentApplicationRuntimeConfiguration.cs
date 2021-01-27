namespace EyeSoft.Windows.Runtime
{
    using Core.Diagnostic;

    internal class FluentApplicationRuntimeConfiguration :
		IApplicationRuntimeConfigurationName,
		IApplicationRuntimeConfigurationDownloadUrl
	{
		public FluentApplicationRuntimeConfiguration(
			string company,
			string applicationName,
			InstallationType installationType,
			string[] subFolders)
		{
			Company = company;
			ApplicationName = applicationName;
			InstallationType = installationType;
			SubFolders = subFolders;
		}

		public string Company { get; private set; }

		public string ApplicationName { get; private set; }

		public InstallationType InstallationType { get; private set; }

		public string[] SubFolders { get; private set; }

		public string DownloadUrlField { get; private set; }

		public string ProjectPathField { get; private set; }

		public IApplicationRuntimeConfigurationDownloadUrl DownloadUrl(string remoteDownloadUrl, string localDownloadUrl = null)
		{
			var debuggerAttached = SystemInspector.Debugger.IsAttached;

			if (localDownloadUrl == null)
			{
				localDownloadUrl = remoteDownloadUrl;
			}

			var downloadUrl = debuggerAttached ? localDownloadUrl : remoteDownloadUrl;

			DownloadUrlField = downloadUrl;

			return this;
		}

		public ApplicationRuntimeConfiguration ProjectPath(string projectPath)
		{
			ProjectPathField = projectPath;
			return new ApplicationRuntimeConfiguration(Company, ApplicationName, DownloadUrlField, ProjectPathField, SubFolders);
		}
	}
}