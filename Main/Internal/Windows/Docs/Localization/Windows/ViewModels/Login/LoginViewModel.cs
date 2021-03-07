namespace EyeSoft.Demo.Localization.Windows.ViewModels.Login
{
    using System.Windows.Input;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Home;
    using EyeSoft.Windows.Model;

    public class LoginViewModel : NavigableViewModel
    {
#if DEBUG
        public LoginViewModel() : base(null)
        {
        }
#endif
        public LoginViewModel(INavigableViewModel navigableViewModel)
            : base(navigableViewModel)
        {
        }

        public ICommand LoginCommand { get; private set; }

        private void Login()
        {
            Navigate(new HomeViewModel(navigableViewModel));
        }
    }
}