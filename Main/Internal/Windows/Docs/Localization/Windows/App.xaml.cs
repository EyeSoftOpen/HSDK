namespace EyeSoft.Demo.Localization.Windows
{
    using System.Globalization;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Main;
    using EyeSoft.Windows.Model.Localization;
    using EyeSoft.Windows.Model.Localization.Providers;

    public partial class App
    {
        public App()
        {
            InitializeComponent();

            var culture = CultureInfo.CreateSpecificCulture("en");

            //ITranslationEngine translationEngine =
            //    new TranslationEngine(
            //        new ResourceTranslationProvider(GetType().Assembly,
            //        CultureInfo.CreateSpecificCulture("it"),
            //        CultureInfo.CreateSpecificCulture("en")));

            ITranslationEngine translationEngine =
                new TranslationEngine(new JsonTranslationProvider(GetType().Assembly))
                {
                    CurrentLanguage = culture
                };

            TranslationManager.Instance = translationEngine;

            var window = new Main
            {
                DataContext = new MainViewModel(translationEngine)
            };

            window.ShowDialog();
        }
    }
}