namespace EyeSoft.Windows.Runtime
{
	public interface IApplicationRuntimeConfigurationName
	{
		IApplicationRuntimeConfigurationDownloadUrl DownloadUrl(string remoteDownloadUrl, string localDownloadUrl = null);
	}
}