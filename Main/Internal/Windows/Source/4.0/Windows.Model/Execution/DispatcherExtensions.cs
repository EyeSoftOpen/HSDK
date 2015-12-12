namespace EyeSoft.Windows.Model
{
	using System;
	using System.Windows.Threading;

	public static class DispatcherExtensions
	{
		public static object Invoke(this Dispatcher dispatcher, Action action)
		{
			return dispatcher.Invoke(action, null);
		}

		public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
		{
			return dispatcher.BeginInvoke(action, null);
		}
	}
}