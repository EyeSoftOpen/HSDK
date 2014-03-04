namespace EyeSoft.Windows.Installer.InstallMate
{
	using System;
	using System.Runtime.InteropServices;

	internal class ApplicationUpdater
	{
		private const string InstallMateDllName = "twu2010ui";

		[DllImport(InstallMateDllName, EntryPoint = "TWUOpenUpdateW")]
		public static extern IntPtr OpenUpdate(IntPtr window);

		[DllImport(InstallMateDllName, EntryPoint = "TWUCloseUpdateW")]
		public static extern uint CloseUpdate(IntPtr window);

		[DllImport(InstallMateDllName, EntryPoint = "TWUCleanupUpdateW")]
		public static extern void CleanupUpdate();

		[DllImport(InstallMateDllName, EntryPoint = "TWUCheckUpdateW", CharSet = CharSet.Unicode)]
		public static extern uint CheckUpdate(IntPtr update, string updateUrl, string pszSection, uint dwFlags, out bool pbUpdateAvailable);

		[DllImport(InstallMateDllName, EntryPoint = "TWUGetUpdateNameW", CharSet = CharSet.Unicode)]
		public static extern string GetUpdateName(IntPtr hUpdate);

		[DllImport(InstallMateDllName, EntryPoint = "TWUGetUpdateVersionW", CharSet = CharSet.Unicode)]
		public static extern string GetUpdateVersion(IntPtr hUpdate);

		[DllImport(InstallMateDllName, EntryPoint = "TWUDownloadUpdateW")]
		public static extern uint DownloadUpdate(IntPtr update, uint dwFlags);

		[DllImport(InstallMateDllName, EntryPoint = "TWUInstallUpdateW")]
		public static extern uint InstallUpdate(IntPtr update, uint dwFlags);

		[DllImport(InstallMateDllName, EntryPoint = "TWURegisterRestartW", CharSet = CharSet.Unicode)]
		public static extern uint RegisterRestart(IntPtr update, string pszAppPath, string pszOptions, uint dwFlags);
	}
}