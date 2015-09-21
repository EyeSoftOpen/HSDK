using System;
using EyeSoft.Timers;

namespace EyeSoft.Architecture.Prototype.Windows.Model.Base
{
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