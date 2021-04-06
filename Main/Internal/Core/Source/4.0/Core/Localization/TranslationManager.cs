namespace EyeSoft.Core.Localization
{
    using System;

    public static class TranslationManager
    {
        private static readonly object lockInstance = new object();

        private static ITranslationEngine translationManager;

        public static ITranslationEngine Instance
        {
            get => translationManager;
            set
            {
                lock (lockInstance)
                {
                    if (translationManager != null)
                    {
                        throw new InvalidOperationException("Cannot set the translation manager more than once.");
                    }

                    translationManager = value;
                }
            }
        }
    }
}