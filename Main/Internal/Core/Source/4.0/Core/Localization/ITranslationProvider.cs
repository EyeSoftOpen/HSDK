namespace EyeSoft.Core.Localization
{
    using System.Collections.Generic;
    using System.Globalization;

    public interface ITranslationProvider
    {
        string Translate(CultureInfo currentLanguage, string key);

        IEnumerable<CultureInfo> Languages { get; }
    }
}