namespace EyeSoft.Windows.Model.Localization.Providers
{
    using System.Collections.Generic;
    using System.Globalization;

    public abstract class TranslationProvider : ITranslationProvider
    {
        public virtual string Translate(CultureInfo currentLanguage, string key)
        {
            var tokens = key.Split('.');

            var typeName = tokens.Length == 2 ? tokens[0] : "Resources";
            var keyName = tokens.Length == 1 ? tokens[0] : tokens[1];

            var translation = Translate(currentLanguage, typeName, keyName);

            return translation;
        }

        protected abstract string Translate(CultureInfo currentLanguage, string resource, string key);

        public IEnumerable<CultureInfo> Languages { get; protected set; }
    }
}