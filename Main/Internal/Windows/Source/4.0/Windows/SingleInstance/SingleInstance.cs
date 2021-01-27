namespace EyeSoft.Windows.SingleInstance
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Security.Permissions;

    public static class SingleInstance
	{
		public static ApplicationMutex ApplicationMutex(
			this Assembly assembly, Action shutdown = null, Process mainProcess = null)
		{
			var applicationInstanceId = assembly.GetApplicationInstanceId();

			var mutex = ApplicationMutexes.CreateOrExisting(applicationInstanceId);

			var applicationMutex = new ApplicationMutex(applicationInstanceId, mutex);

			if (!applicationMutex.IsAlreadyRunning)
			{
				return applicationMutex;
			}

			if (mainProcess == null)
			{
				mainProcess = Process.GetCurrentProcess();
			}

			var otherApplicationInstance = FindSingleInstanceProcess(mainProcess);

			if (otherApplicationInstance == null)
			{
				return applicationMutex.AsSingle();
			}

			var handle = otherApplicationInstance.MainWindowHandle.ToInt32();
			WindowsHelper.ForceForegroundWindow(handle);

			if (shutdown != null)
			{
				shutdown();
			}

			return applicationMutex;
		}

		private static Process FindSingleInstanceProcess(Process mainProcess)
		{
			var processName = mainProcess.ProcessName;

			var processes = Process.GetProcessesByName(processName);

			var otherProcess = processes.SingleOrDefault(process => process.MainWindowHandle != mainProcess.MainWindowHandle);

			return otherProcess;
		}

		[SecurityCritical]
		private static string GetApplicationInstanceId(this Assembly entry)
		{
			var set = new PermissionSet(PermissionState.None);
			set.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
			set.AddPermission(new SecurityPermission(SecurityPermissionFlag.UnmanagedCode));
			set.Assert();

			var typeLibGuidForAssembly = Marshal.GetTypeLibGuidForAssembly(entry);
			var strArray = entry.GetName().Version.ToString().Split(".".ToCharArray());
			PermissionSet.RevertAssert();

			return typeLibGuidForAssembly + strArray[0] + "." + strArray[1];
		}
	}
}