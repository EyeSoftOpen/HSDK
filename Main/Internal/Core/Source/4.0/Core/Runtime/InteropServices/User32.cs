namespace EyeSoft.Core.Runtime.InteropServices
{
    using System;
    using System.Runtime.InteropServices;

    public static class User32
	{
		public const int Show = 5;
		public const int Restore = 9;
		public const uint QuitProcess = 0x12;

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetTopWindow(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hwnd, out uint processId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(HandleRef hWnd, uint message, IntPtr wParam, IntPtr lParam);

		[DllImport("User32.dll")]
		public static extern int GetForegroundWindow();

		[DllImport("User32.dll")]
		public static extern int SetForegroundWindow(int handle);

		[DllImport("user32.dll")]
		public static extern int GetWindowThreadProcessId(int window, int processId);

		[DllImport("User32.dll")]
		public static extern int AttachThreadInput(int idAttach, int idAttachTo, int fAttach);

		[DllImport("User32.dll")]
		public static extern int IsIconic(int handle);

		[DllImport("User32.dll")]
		public static extern int ShowWindow(int handle, int showMode);

		[DllImport("User32.dll")]
		public static extern int SetActiveWindow(int handle);

		[DllImport("User32.dll")]
		public static extern int BringWindowToTop(int handle);
	}
}