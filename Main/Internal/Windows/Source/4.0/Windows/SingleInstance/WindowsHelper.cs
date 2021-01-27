namespace EyeSoft.Windows.SingleInstance
{
    using Core.Runtime.InteropServices;

    public static class WindowsHelper
	{
		public static bool ForceForegroundWindow(int handle)
		{
			var foregroundWnd = User32.GetForegroundWindow();

			if (handle == foregroundWnd)
			{
				return true;
			}

			int ret;

			var threadId1 = User32.GetWindowThreadProcessId(foregroundWnd, 0);
			var threadId2 = User32.GetWindowThreadProcessId(handle, 0);

			if (threadId1 != threadId2)
			{
				User32.AttachThreadInput(threadId1, threadId2, 1);
				ret = User32.SetForegroundWindow(handle);
				User32.AttachThreadInput(threadId1, threadId2, 0);
			}
			else
			{
				ret = User32.SetForegroundWindow(handle);
			}

			var showWindowMode = User32.IsIconic(handle) == 0 ? User32.Restore : User32.Show;

			User32.ShowWindow(handle, showWindowMode);
			User32.SetActiveWindow(handle);
			User32.BringWindowToTop(handle);

			return ret != 0;
		}
	}
}