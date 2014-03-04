namespace EyeSoft.Demo.Navigation.Windows.ViewModels
{
	using System;

	using EyeSoft.Timers;
	using EyeSoft.Windows.Model;

	public class TimeViewModel : NavigableViewModel
	{
		private readonly ITimer timer;

		public TimeViewModel(INavigableViewModel navigableViewModel)
			: base(navigableViewModel)
		{
			timer = TimerFactory.StartAndExecute(TimeSpan.FromSeconds(1), () => CurrentTime = DateTime.Now);
		}

		public DateTime CurrentTime
		{
			get { return GetProperty<DateTime>(); }
			private set { SetProperty(value); }
		}

		protected override void Release()
		{
			timer.Dispose();
			base.Release();
		}
	}
}