namespace EyeSoft.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using Providers;

    public class TranslationEngine : ITranslationEngine
    {
        public TranslationEngine(Assembly assembly) : this(new ResourceTranslationProvider(assembly))
        {
        }

        public TranslationEngine(ITranslationProvider translationProvider)
        {
            TranslationProvider = translationProvider;
        }

        public event EventHandler<CultureChangedEventArgs> LanguageChanged;

        public virtual CultureInfo CurrentLanguage
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

        protected virtual void OnLanguageChanged(CultureInfo cultureInfo)
        {
            LanguageChanged?.Invoke(this, new CultureChangedEventArgs(cultureInfo));
        }
    }
}