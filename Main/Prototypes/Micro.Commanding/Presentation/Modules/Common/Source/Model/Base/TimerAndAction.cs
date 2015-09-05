namespace Model
{
	using System;

	using EyeSoft.Timers;

	internal class TimerAndAction
	{
		public TimerAndAction(ITimer timer, Action action)
		{
			Creation = DateTime.Now;
			Timer = timer;
			Action = action;
		}

		public DateTime Creation { get; }

		public ITimer Timer { get; }

		public Action Action { get; }
	}
}