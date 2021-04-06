namespace EyeSoft.Windows.Model.Localization
{
    using System;
    using System.Windows;
    using EyeSoft.Core.Localization;

    public class LanguageChangedEventManager : WeakEventManager
    {
        public static void AddListener(ITranslationEngine source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedAddListener(source, listener);
        }

        public static void RemoveListener(ITranslationEngine source, IWeakEventListener listener)
        {
            CurrentManager.ProtectedRemoveListener(source, listener);
        }

        private void OnLanguageChanged(object sender, EventArgs e)
        {
            DeliverEvent(sender, e);
        }

        protected override void StartListening(object source)
        {
            if (!(source is ITranslationEngine engine))
            {
                return;
            }

            engine.LanguageChanged += OnLanguageChanged;
        }

        protected override void StopListening(object source)
        {
            if (!(source is ITranslationEngine engine))
            {
                return;
            }

            engine.LanguageChanged -= OnLanguageChanged;
        }

        private static LanguageChangedEventManager CurrentManager
        {
            get
            {
                var managerType = typeof(LanguageChangedEventManager);
                var manager = (LanguageChangedEventManager)GetCurrentManager(managerType);

                if (manager != null)
                {
                    return manager;
                }

                manager = new LanguageChangedEventManager();
                SetCurrentManager(managerType, manager);

                return manager;
            }
        }
    }
}