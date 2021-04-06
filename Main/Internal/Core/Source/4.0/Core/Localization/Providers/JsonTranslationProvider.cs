namespace EyeSoft.Core.Localization.Providers
{
    using System.Globalization;
    using System.Linq;
    using Newtonsoft.Json;

    public abstract class JsonTranslationProvider : TranslationProvider
    {
        private readonly Translation translation;

        protected JsonTranslationProvider(string json)
        {
            translation = GetTranslation(json);
        }

        protected Translation GetTranslation(string json)
        {
            var localTranslation = JsonConvert.DeserializeObject<Translation>(json);

            return localTranslation;
        }

        protected override string Translate(CultureInfo currentLanguage, string resource, string key)
        {
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
