namespace EyeSoft.Windows.Model.Localization.Providers
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;

    public class ResourceTranslationProvider : TranslationProvider
    {
        private readonly Assembly assembly;

        public ResourceTranslationProvider(Assembly assembly, params CultureInfo[] languages)
        {
            this.assembly = assembly ?? throw new ArgumentNullException(nameof(assembly));

            if (languages == null || !languages.Any())
            {
                throw new InvalidOperationException("The supported languages cannot be null or empty.");
            }

            Languages = languages;
        }

        protected override string Translate(CultureInfo currentLanguage, string resource, string key)
        {
            var type = assembly.GetTypes().SingleOrDefault(x => x.Name == resource);

            if (type == null)
            {
                throw new InvalidOperationException($"Cannot find a type with name {resource}.");
            }

            var keyValue = (string)type.GetProperty(key, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)?.GetValue(null);

            return keyValue;
        }
    }
}