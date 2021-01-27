namespace EyeSoft.Demo.Navigation.Windows.ViewModels
{
	using System.Windows.Input;

	using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.ViewModels.Navigation;

    public class WelcomeViewModel : NavigableViewModel
	{
		public WelcomeViewModel(INavigableViewModel navigableViewModel)
			: base(navigableViewModel)
		{
		}

		public ICommand CloseCommand { get; private set; }
	}
}