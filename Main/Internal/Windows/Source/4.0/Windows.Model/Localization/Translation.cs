namespace EyeSoft.Windows.Model.Localization
{
    using System.Collections.Generic;
    using System.Globalization;

    public class Translation
    {
        public Translation(
            IEnumerable<CultureInfo> cultures,
            IReadOnlyDictionary<string, IReadOnlyList<string>> translations)
        {
            Cultures = cultures;
            Translations = translations;
        }

        public IEnumerable<CultureInfo> Cultures { get; }

        public IReadOnlyDictionary<string, IReadOnlyList<string>> Translations { get; }
    }
}