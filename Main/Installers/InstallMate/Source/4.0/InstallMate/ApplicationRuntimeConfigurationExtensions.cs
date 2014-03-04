namespace EyeSoft.Windows.Installer.InstallMate
{
	using System;
	using System.Windows;

	using EyeSoft.Windows.Runtime;

	public static class ApplicationRuntimeConfigurationExtensions
	{
		public static InstallMateInstaller ToInstallMate(this ApplicationRuntimeConfiguration configuration, Func<Window> createDialog)
		{
			var installer = new InstallMateInstaller(configuration, createDialog);
			return installer;
		}
	}
}