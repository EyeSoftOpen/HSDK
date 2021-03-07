namespace EyeSoft.Demo.Localization.Windows.ViewModels.Main
{
    using System;
    using EyeSoft.Windows.Model;
    using EyeSoft.Windows.Model.Localization;
    using MahApps.Metro.IconPacks;

    public class MenuItemViewModel : ViewModel
    {
        private readonly ITranslationEngine translationEngine;
        private readonly Action navigate;
        private readonly string label;

        public MenuItemViewModel(ITranslationEngine translationEngine, string label, PackIconFontAwesomeKind icon,
            Action navigate)
        {
            this.translationEngine = translationEngine;
            this.navigate = navigate;
            this.label = label;
            Icon = icon;

            translationEngine.LanguageChanged += LanguageChanged;

            LanguageChanged();
        }

        public string Label
        {
            get => GetProperty<string>();
            private set => SetProperty(value);
        }

        public PackIconFontAwesomeKind Icon { get; }

        public void Navigate()
        {
            navigate();
        }

        private void LanguageChanged(object sender, CultureChangedEventArgs e)
        {
            LanguageChanged();
        }

        private void LanguageChanged()
        {
            Label = translationEngine.Translate(label);
        }
    }
}