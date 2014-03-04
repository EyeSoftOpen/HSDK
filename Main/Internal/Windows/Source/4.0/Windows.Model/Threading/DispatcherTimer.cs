namespace EyeSoft.Windows.Model.Threading
{
	using System;

	using EyeSoft.Timers;

	public class DispatcherTimer : ITimer
	{
		private readonly System.Windows.Threading.DispatcherTimer timer;

		public DispatcherTimer(int interval, Action action, bool oneTimeAction = false)
		{
			timer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, interval) };

			if (oneTimeAction)
			{
				timer.Tick += (s, e) =>
					{
						Stop();
						action();
						Dispose();
					};

				return;
			}

			var localAction = action;

			timer.Tick += (s, e) => localAction();
		}

		public DispatcherTimer(int interval, Action<ITimer> action)
		{
			timer = new System.Windows.Threading.DispatcherTimer { Interval = new TimeSpan(0, 0, 0, 0, interval) };

			timer.Tick += (s, e) => action(this);
		}

		public double Interval
		{
			get
			{
				return timer.Interval.Milliseconds;
			}
			set
			{
				timer.Interval = new TimeSpan(0, 0, 0, 0, (int)value);
			}
		}

		public ITimer Start()
		{
			timer.Start();
			return this;
		}

		public void Stop()
		{
			timer.Stop();
		}

		public void Dispose()
		{
		}
	}
}