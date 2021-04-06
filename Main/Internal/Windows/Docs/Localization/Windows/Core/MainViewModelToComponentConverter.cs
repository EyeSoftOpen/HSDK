namespace EyeSoft.Demo.Localization.Windows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Home;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Login;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Settings;
    using EyeSoft.Windows.Converters;

    public class MainViewModelToComponentConverter : ViewModelToComponentConverter
    {
        private static readonly Type defaultViewModelType = typeof(LoginViewModel);
        private static readonly Assembly assembly = typeof(MainViewModelToComponentConverter).Assembly;

        private static readonly IDictionary<Type, Uri> viewModelTypeToResource =
            new Dictionary<Type, Uri>
                {
                    { defaultViewModelType, EyeSoft.Windows.Component.ToUri(@"Login\Login", "Views", assembly) },
                    { typeof(HomeViewModel), EyeSoft.Windows.Component.ToUri(@"Home\Home", "Views", assembly) },
                    { typeof(SettingsViewModel), EyeSoft.Windows.Component.ToUri(@"Settings\Settings", "Views", assembly) }
                };

        protected override Uri Get(Type viewModelType)
        {
            var containsKey = viewModelTypeToResource.ContainsKey(viewModelType);

            var uri = containsKey ? viewModelTypeToResource[viewModelType] : viewModelTypeToResource[defaultViewModelType];

            return uri;
        }
    }
}