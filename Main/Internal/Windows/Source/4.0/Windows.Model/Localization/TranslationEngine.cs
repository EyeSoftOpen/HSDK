namespace EyeSoft.Windows.Model.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;
    using Providers;

    public class TranslationEngine : ITranslationEngine
    {
        private bool frameworkElementSet;

        public TranslationEngine(Assembly assembly)
            : this(new ResourceTranslationProvider(assembly))
        {
        }

        public TranslationEngine(ITranslationProvider translationProvider)
        {
            TranslationProvider = translationProvider;
        }

        public event EventHandler<CultureChangedEventArgs> LanguageChanged;

        public CultureInfo CurrentLanguage
        {
            get => Thread.CurrentThread.CurrentUICulture;
            set
            {
                if (Equals(value, Thread.CurrentThread.CurrentUICulture))
                {
                    return;
                }

                Thread.CurrentThread.CurrentCulture = value;
                Thread.CurrentThread.CurrentUICulture = value;

                if (!frameworkElementSet)
                {
                    var frameworkElement = typeof(FrameworkElement);
                    var propertyMetadata = new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(value.Name));
                    
                    FrameworkElement.LanguageProperty.OverrideMetadata(frameworkElement, propertyMetadata);
                }

                frameworkElementSet = true;

                OnLanguageChanged(value);
            }
        }

        public IEnumerable<CultureInfo> Languages => TranslationProvider != null ? TranslationProvider.Languages : Enumerable.Empty<CultureInfo>();

        public ITranslationProvider TranslationProvider { get; }

        public string Translate(string key)
        {
            var translation = TranslationProvider?.Translate(CurrentLanguage, key);

            if (translation != null)
            {
                return translation;
            }

            var tokens = key.Split('.');
            var keyName = tokens.Length == 1 ? tokens[0] : tokens[1];

            translation = $"!{keyName}!";

            return translation;
        }

        private void OnLanguageChanged(CultureInfo cultureInfo)
        {
            LanguageChanged?.Invoke(this, new CultureChangedEventArgs(cultureInfo));
        }
    }
}