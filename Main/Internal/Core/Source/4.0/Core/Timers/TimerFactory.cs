namespace EyeSoft.Core.Timers
{
    using System;

    public static class TimerFactory
	{
		private static readonly Singleton<ITimerFactory> singletonInstance = new Singleton<ITimerFactory>(() => new DefaultTimerFactory());

		public static void Set(ITimerFactory instance)
		{
			singletonInstance.Set(() => instance);
		}

		public static ITimerFactory Create()
		{
			return singletonInstance.Instance;
		}

		public static ITimer Create(TimeSpan timespan, Action action)
		{
			return singletonInstance.Instance.Create(timespan.ToMilliseconds(), action);
		}

		public static ITimer Create(int milliseconds, Action action)
		{
			return singletonInstance.Instance.Create(milliseconds, action);
		}

		public static ITimer StartAndExecute(TimeSpan timespan, Action action)
		{
			return StartAndExecute(timespan.ToMilliseconds(), action);
		}

		public static ITimer StartAndExecute(int milliseconds, Action action)
		{
			action();
			return Start(milliseconds, action);
		}

		public static ITimer Start(TimeSpan timespan, Action action)
		{
			return Start(timespan.ToMilliseconds(), action);
		}

		public static ITimer Start(int milliseconds, Action action)
		{
			var timer = singletonInstance.Instance.Create(milliseconds, action);

			timer.Start();

			return timer;
		}

		public static void DelayedExecute(TimeSpan timespan, Action action)
		{
			DelayedExecute(timespan.ToMilliseconds(), action);
		}

		public static void DelayedExecute(int milliseconds, Action action)
		{
			singletonInstance.Instance.DelayedExecute(milliseconds, action);
		}

		private static int ToMilliseconds(this TimeSpan timespan)
		{
			return (int)timespan.TotalMilliseconds;
		}
	}
}