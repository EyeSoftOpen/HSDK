namespace EyeSoft.Core.Timers
{
    using System;

    public interface ITimerFactory
	{
		ITimer Create(int interval, Action action);

		void Create(int interval, Action<ITimer> action);

		void DelayedExecute(int interval, Action action);
	}
}