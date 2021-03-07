namespace EyeSoft.Windows.Model.Localization
{
    using System;
    using System.Windows.Data;
    using System.Windows.Markup;

    public class TranslateExtension : MarkupExtension
    {
        private readonly string key;

        public TranslateExtension(string key)
        {
            this.key = key;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding("Value")
            {
                Source = new TranslationData(key)
            };

            var value = binding.ProvideValue(serviceProvider);

            return value;
        }
    }
}