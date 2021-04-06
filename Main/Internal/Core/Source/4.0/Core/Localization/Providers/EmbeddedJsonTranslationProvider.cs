namespace EyeSoft.Core.Localization.Providers
{
    using System.Reflection;
    using Reflection;

    public class EmbeddedJsonTranslationProvider : JsonTranslationProvider
    {
        public EmbeddedJsonTranslationProvider(Assembly assembly) : base(ReadJson(assembly))
        {
        }

        private static string ReadJson(Assembly assembly)
        {
            var json = assembly.ReadResourceText("Data.Translation.json");

            return json;
        }
    }
}