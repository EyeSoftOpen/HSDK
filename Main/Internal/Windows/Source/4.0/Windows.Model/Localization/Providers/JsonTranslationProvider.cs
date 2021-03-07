namespace EyeSoft.Windows.Model.Localization.Providers
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;
    using Reflection;

    public class JsonTranslationProvider : TranslationProvider
    {
        private readonly Translation translation;
        private readonly IReadOnlyDictionary<CultureInfo, int> languageDictionary;

        public JsonTranslationProvider(Assembly assembly) : this(ReadJson(assembly))
        {
        }

        public JsonTranslationProvider(string json)
        {
            translation = JsonConvert.DeserializeObject<Translation>(json);

            Languages = translation.Cultures;
                
            languageDictionary = Languages
                .Select((x, index) => new { Index = x, Value = index })
                .ToDictionary(k => k.Index, v => v.Value);
        }

        private static string ReadJson(Assembly assembly)
        {
            var json = assembly.ReadResourceText("Data.Translation.json");

            return json;
        }

        protected override string Translate(CultureInfo currentLanguage, string resource, string key)
        {
            var translations = translation.Translations;

            if (!translations.ContainsKey(key))
            {
                return null;
            }

            var index = languageDictionary[currentLanguage];

            var value = translations[key][index];

            return value;
        }
    }
}
