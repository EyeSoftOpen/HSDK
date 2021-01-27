namespace EyeSoft.Core.Timers
{
    using System;

    public interface ITimer : IDisposable
	{
		double Interval { get; set; }

		ITimer Start();

		void Stop();
	}
}