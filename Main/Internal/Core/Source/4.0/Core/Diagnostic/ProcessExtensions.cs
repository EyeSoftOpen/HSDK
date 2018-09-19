namespace EyeSoft.Diagnostic
{
	using System;
	using System.Diagnostics;
	using System.Runtime.InteropServices;

	using EyeSoft.Runtime.InteropServices;

	public static class ProccessExtensions
	{
		public static void SendStopMessage(this Process process)
		{
			SendStopMessage(process.Id);
		}

		public static void SendStopMessage(int processId)
		{
			for (var ptr = User32.GetTopWindow(IntPtr.Zero); ptr != IntPtr.Zero; ptr = User32.GetWindow(ptr, 2))
			{
				uint num;
				User32.GetWindowThreadProcessId(ptr, out num);

				if (processId != num)
				{
					continue;
				}

				var hWnd = new HandleRef(null, ptr);
				User32.PostMessage(hWnd, User32.QuitProcess, IntPtr.Zero, IntPtr.Zero);
				return;
			}
		}
	}
}
