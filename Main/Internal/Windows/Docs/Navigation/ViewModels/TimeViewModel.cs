namespace EyeSoft.Demo.Navigation.Windows.ViewModels
{
	using System;
    using Core.Timers;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels.Navigation;

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
			get => GetProperty<DateTime>();
            private set => SetProperty(value);
        }

		protected override void Release()
		{
			timer.Dispose();
			base.Release();
		}
	}
}