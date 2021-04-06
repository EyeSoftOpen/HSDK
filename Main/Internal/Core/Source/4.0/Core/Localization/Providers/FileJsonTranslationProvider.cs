namespace EyeSoft.Core.Localization.Providers
{
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using Newtonsoft.Json;

    public class FileJsonTranslationProvider : TranslationProvider
    {
        private readonly string path;

        public FileJsonTranslationProvider(string path)
        {
            this.path = path;
        }

        protected override string Translate(CultureInfo currentLanguage, string resource, string key)
        {
            var json = File.ReadAllText(path);

            var translation = JsonConvert.DeserializeObject<Translation>(json);

            var translations = translation.Translations;

            if (!translations.ContainsKey(key))
            {
                return null;
            }

            var languageDictionary = translation.Cultures
                .Select((x, index) => new { Index = x, Value = index })
                .ToDictionary(k => k.Index, v => v.Value);

            var languageIndex = languageDictionary[currentLanguage];

            var value = translations[key][languageIndex];

            return value;
        }
    }
}