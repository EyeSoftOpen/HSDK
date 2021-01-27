namespace EyeSoft.Core.Timers
{
    using System;

    public class DefaultTimerFactory : ITimerFactory
	{
		public ITimer Create(int interval, Action action)
		{
			return new Timer(interval, action);
		}

		public void Create(int interval, Action<ITimer> action)
		{
			new Timer(interval, action).Start();
		}

		public void DelayedExecute(int interval, Action action)
		{
			new Timer(interval, action, true).Start();
		}
	}
}