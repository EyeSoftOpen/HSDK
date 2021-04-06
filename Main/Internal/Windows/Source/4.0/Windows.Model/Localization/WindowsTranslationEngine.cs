namespace EyeSoft.Windows.Model.Localization
{
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Windows;
    using System.Windows.Markup;
    using Core.Localization;

    public class WindowsTranslationEngine : TranslationEngine
    {
        private bool frameworkElementSet;

        public WindowsTranslationEngine(Assembly assembly) : base(assembly)
        {
        }

        public WindowsTranslationEngine(ITranslationProvider translationProvider) : base(translationProvider)
        {
        }

        public override CultureInfo CurrentLanguage
        {
            get => base.CurrentLanguage;
            set
            {
                if (Equals(value, Thread.CurrentThread.CurrentUICulture))
                {
                    return;
                }

                base.CurrentLanguage = value;

                if (!frameworkElementSet)
                {
                    var frameworkElement = typeof(FrameworkElement);
                    var propertyMetadata = new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(value.Name));

                    FrameworkElement.LanguageProperty.OverrideMetadata(frameworkElement, propertyMetadata);
                }

                frameworkElementSet = true;

                OnLanguageChanged(value);
            }
        }
    }
}