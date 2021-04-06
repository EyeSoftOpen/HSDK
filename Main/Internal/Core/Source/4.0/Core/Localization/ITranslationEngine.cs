namespace EyeSoft.Core.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    public interface ITranslationEngine
    {
        event EventHandler<CultureChangedEventArgs> LanguageChanged;
        
        CultureInfo CurrentLanguage { get; set; }

        IEnumerable<CultureInfo> Languages { get; }

        ITranslationProvider TranslationProvider { get; }

        string Translate(string key);
    }
}