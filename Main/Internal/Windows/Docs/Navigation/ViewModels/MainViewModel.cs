namespace EyeSoft.Demo.Navigation.Windows.ViewModels
{
	using System.Windows.Input;

	using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels.Navigation;

    public class MainViewModel : ShellViewModel
    {
		public MainViewModel()
		{
			Navigate(new WelcomeViewModel(this));
		}

		public ICommand GoToWelcomeCommand { get; private set; }

		public ICommand GoToTimeCommand { get; private set; }

		protected void GoToWelcome()
		{
			Navigate(new WelcomeViewModel(this));
		}

		protected void GoToTime()
		{
			Navigate(new TimeViewModel(this));
		}
    }
}