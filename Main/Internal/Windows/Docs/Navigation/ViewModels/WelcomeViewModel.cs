namespace EyeSoft.Demo.Navigation.Windows.ViewModels
{
	using System.Windows.Input;

	using EyeSoft.Windows.Model;

    public class WelcomeViewModel : NavigableViewModel
	{
		public WelcomeViewModel(INavigableViewModel navigableViewModel)
			: base(navigableViewModel)
		{
		}

		public ICommand CloseCommand { get; private set; }
	}
}