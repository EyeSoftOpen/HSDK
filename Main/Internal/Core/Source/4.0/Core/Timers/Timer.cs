namespace EyeSoft.Timers
{
    using System;

    public class Timer : ITimer
	{
		private readonly System.Timers.Timer timer;

		public Timer(double interval, Action action, bool oneTimeAction = false)
		{
			timer = new System.Timers.Timer(interval);

			if (oneTimeAction)
			{
				timer.Elapsed +=
					(s, e) =>
						{
							Stop();
							action();
							Dispose();
						};

				return;
			}

			var localAction = action;

			timer.Elapsed += (s, e) => localAction();
		}

		public Timer(int interval, Action<ITimer> action)
		{
			timer = new System.Timers.Timer(interval);

			timer.Elapsed += (s, e) => action(this);
		}

		public double Interval
		{
			get => timer.Interval;
            set => timer.Interval = value;
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
			timer?.Dispose();
		}
	}
}