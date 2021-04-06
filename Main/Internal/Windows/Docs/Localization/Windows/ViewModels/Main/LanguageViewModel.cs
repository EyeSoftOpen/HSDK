namespace EyeSoft.Demo.Localization.Windows.ViewModels.Main
{
    using EyeSoft.Core.Localization;
    using EyeSoft.Windows.Model;

    public class LanguageViewModel : ViewModel
    {
        private readonly ITranslationEngine translationEngine;
        private readonly string name;

        public LanguageViewModel(ITranslationEngine translationEngine, string name, string twoLetter, bool isEnabled)
        {
            this.translationEngine = translationEngine;
            this.name = name;
            TwoLetter = twoLetter;
            IsEnabled = isEnabled;

            translationEngine.LanguageChanged += LanguageChanged;
            LanguageChanged();
        }

        public string Name
        {
            get => GetProperty<string>();
            private set => SetProperty(value);
        }

        public string TwoLetter { get; }

        public bool IsEnabled
        {
            get => GetProperty<bool>();
            set => SetProperty(value);
        }

        private void LanguageChanged(object sender, CultureChangedEventArgs e)
        {
            LanguageChanged();
        }

        private void LanguageChanged()
        {
            Name = translationEngine.Translate(name);
        }
    }
}