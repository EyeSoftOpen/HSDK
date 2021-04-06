namespace EyeSoft.Demo.Localization.Windows
{
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using EyeSoft.Core.Localization;
    using EyeSoft.Core.Localization.Providers;
    using EyeSoft.Demo.Localization.Windows.ViewModels.Main;
    using EyeSoft.Windows.Model.Localization;

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
                new WindowsTranslationEngine(new FileJsonTranslationProvider(Path.Combine(new FileInfo(Assembly.GetExecutingAssembly().Location).Directory.Parent.Parent.FullName, "Data", "Translation.json")))
                //new WindowsTranslationEngine(new EmbeddedJsonTranslationProvider(GetType().Assembly))
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