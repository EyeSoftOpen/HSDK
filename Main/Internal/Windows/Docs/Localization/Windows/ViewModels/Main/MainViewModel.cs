namespace EyeSoft.Demo.Localization.Windows.ViewModels.Main
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Input;
    using EyeSoft.Core.Localization;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Home;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Login;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Settings;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.Localization;
    using MahApps.Metro.Controls;
    using MahApps.Metro.IconPacks;

    public class MainViewModel : ShellViewModel
    {
        private readonly ITranslationEngine translationEngine;

        public MainViewModel(ITranslationEngine translationEngine)
        {
            this.translationEngine = translationEngine;
            Menu = new[]
               {
                   new MenuItemViewModel(translationEngine, "Home", PackIconFontAwesomeKind.HomeSolid, () => Navigate(new HomeViewModel(this))),
                   new MenuItemViewModel(translationEngine, "Settings", PackIconFontAwesomeKind.CogSolid, () => Navigate(new SettingsViewModel(this)))
               };

            Languages = new[]
                {
                    new LanguageViewModel(translationEngine, "Italian", "it", true),
                    new LanguageViewModel(translationEngine, "English", "en", false)
                };

            SelectedMenuItem = Menu.First();

            Property(() => Content).OnChanged(x => DisplayMode = x is LoginViewModel ? SplitViewDisplayMode.Overlay : SplitViewDisplayMode.CompactOverlay);
            Property(() => SelectedMenuItem).OnChanged(x => x.Navigate());

            Navigate(new LoginViewModel(this));
        }

        public IEnumerable<MenuItemViewModel> Menu { get; }

        public MenuItemViewModel SelectedMenuItem
        {
            get => GetProperty<MenuItemViewModel>();
            set => SetProperty(value);
        }

        public SplitViewDisplayMode DisplayMode
        {
            get => GetProperty<SplitViewDisplayMode>();
            private set => SetProperty(value);
        }

        public IEnumerable<LanguageViewModel> Languages { get; }

        public ICommand ChangeLanguageCommand { get; private set; }

        protected void SyncChangeLanguage(string languageName)
        {
            foreach (var language in Languages)
            {
                language.IsEnabled = true;
            }

            Languages.Single(x => x.TwoLetter == languageName).IsEnabled = false;

            var culture = CultureInfo.CreateSpecificCulture(languageName);

            translationEngine.CurrentLanguage = culture;
        }
    }
}