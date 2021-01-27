namespace EyeSoft.Windows.Model.Threading
{
	using System;
    using Core.Timers;

    public class DispatcherTimerFactory : ITimerFactory
	{
		public ITimer Create(int interval, Action action)
		{
			return new DispatcherTimer(interval, action);
		}

		public void Create(int interval, Action<ITimer> action)
		{
			new DispatcherTimer(interval, action).Start();
		}

		public void DelayedExecute(int interval, Action action)
		{
			new DispatcherTimer(interval, action, true).Start();
		}
	}
}